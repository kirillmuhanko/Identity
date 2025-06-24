```bash
dotnet ef migrations add InitialCreate --project src/Company.Identity.Persistence --startup-project src/Company.Identity.Api --output-dir IdentityDb/Migrations
dotnet ef database update --project src/Company.Identity.Persistence --startup-project src/Company.Identity.Api 
```