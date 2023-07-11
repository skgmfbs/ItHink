dotnet new webapi -minimal -n MiniAPI
cd MiniAPI
code .
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 7.0.3
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package ibm-mq-client
dotnet add package IBMXMSDotnetClient
dotnet tool install --global dotnet-ef
dotnet ef migrations add initial
dotnet ef database update
dotnet dev-certs https --trust
dotnet user-secrets init
dotnet user-secrets set "UserId" "sa"
dotnet user-secrets set "Password" "P@ssw0rd"
docker-compose up -d --build