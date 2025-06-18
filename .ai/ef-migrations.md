```bash
dotnet ef migrations add InitialCreate --project src/Company.Identity.Persistence --startup-project src/Company.Identity.Api
dotnet ef database update --project src/Company.Identity.Persistence --startup-project src/Company.Identity.Api
```