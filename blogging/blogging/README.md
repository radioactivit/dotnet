# Commande à jour
    dotnet ef dbcontext scaffold 'Server=127.0.0.1,1433;Database=Blogging;User ID=SA;Password=yourStrong(!)Password' Microsoft.EntityFrameworkCore.SqlServer -o Model
    dotnet aspnet-codegenerator controller -name BlogController -m Blog -dc BloggingContext