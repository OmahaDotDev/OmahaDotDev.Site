

## Migrations

Create a migration:
`dotnet ef migrations add InitialMigration --project .\OmahaDotDev.ResourceAccess\ --startup-project .\OmahaDotDev.WebSite\ --context SiteDbContext --output-dir Database\Migrations`

Apply Migrations to Database: 
 `dotnet ef database update --project .\OmahaDotDev.ResourceAccess\ --startup-project .\OmahaDotDev.WebSite\ --context SiteDbContext`

 close connections and drop database:
 `
 ALTER DATABASE "aspnet-OmahaDotDev.WebSite-53bc9b9d-9d6a-45d4-8429-2a2761773502" SET SINGLE_USER WITH ROLLBACK 
IMMEDIATE
GO
drop database "aspnet-OmahaDotDev.WebSite-53bc9b9d-9d6a-45d4-8429-2a2761773502"
 `