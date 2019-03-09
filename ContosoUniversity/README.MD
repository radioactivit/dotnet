Pour lancer une BDD mssql2017 en DOCKER
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d --name mssql2017 mcr.microsoft.com/mssql/server:2017-latest
docker start mssql2017

Dans appsettings.json :
Server=127.0.0.1,1433;Database=ContosoUniversity;User ID=SA;Password=yourStrong(!)Password