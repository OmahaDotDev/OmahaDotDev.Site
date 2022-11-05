using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OmahaDotDev.Manager;
using OmahaDotDev.Model.Common;
using OmahaDotDev.WebSite.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddManager(new SiteConfiguration(connectionString));

#region support for NSwag 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(doc =>
{
    doc.Version = "v1";
    doc.Title = "wut";
});
#endregion 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


#region NSwag support
app.UseOpenApi();
app.UseSwaggerUi3();
#endregion


app.MapManager();
app.MapRazorPages();



app.Run();
