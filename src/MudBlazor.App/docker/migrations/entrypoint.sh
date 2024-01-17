#!/usr/bin/env bash
set -exo pipefail
#printenv
dotnet new tool-manifest --force
dotnet tool restore
dotnet tool list dotnet-ef || dotnet tool install dotnet-ef
# dotnet add package Microsoft.EntityFrameworkCore.Design
# dotnet add package Microsoft.EntityFrameworkCore.SQLite
# dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
# dotnet add package Microsoft.EntityFrameworkCore.SqlServer
# dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet ef migrations add InitialCreate
dotnet ef database update -v
dotnet ef migrations list --no-build
