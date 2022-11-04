

## Migrations

Create a migration:
`dotnet ef migrations add InitialMigration --project .\OmahaDotDev.ResourceAccess\ --startup-project .\OmahaDotDev.WebSite\ --context SiteDbContext --output-dir Database\Migrations`

Apply Migrations to Database: 
