using Hero4Hire.TimeUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OmahaDotDev.Manager;
using OmahaDotDev.Model.Common;
using OmahaDotDev.Model.Common.Exceptions;
using OmahaDotDev.WebSite.Auth;
using OmahaDotDev.WebSite.Data;
using OmahaDotDev.WebSite.ErrorHandling;
using System.Net;
using System.Security.Claims;

//https://code-maze.com/global-error-handling-aspnetcore/
//https://stackoverflow.com/questions/65729903/trigger-exception-handler-with-status-code-from-middleware

namespace OmahaDotDev.WebSite
{

    public partial class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddSingleton<IExceptionFilter, ForbiddenExceptionFilter>();
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityDbContext>();
            builder.Services.AddRazorPages();

            builder.Services.AddManager(new SiteConfiguration(connectionString));
            builder.Services.AddTimeUtility();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<AmbientContext>(provider =>
            {
                var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
                return new AmbientContext()
                {
                    UserId = httpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    IsLoggedIn = httpContext?.User.Identity?.IsAuthenticated == true,
                };
            });

            builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Custom", policy =>
                    policy.Requirements.Add(new CustomAuthorizationRequirement()));
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // app.UseMigrationsEndPoint();
                //app.UseSwagger();
                //app.UseSwaggerUI();

            }
            else
            {
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler(c =>
            {
                c.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    var exception = exceptionHandlerPathFeature?.Error;

                    if (exception is ForbiddenException e)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.WriteAsJsonAsync()
                        {
                            StatusCode = (int)HttpStatusCode.Forbidden,
                            Value = _hostEnvironment.IsDevelopment() ? e.Message : "Forbidden"
                        };
                    }
                });
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();



            app.MapManager();
            app.MapRazorPages();

            app.MapGet("/helloworld", (AmbientContext context) =>
                {
                    return "Hello World!";
                })
                .RequireAuthorization("Custom");

            app.Run();
        }

    }
}



