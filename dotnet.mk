.PHONY: dotnet dotnet-app dotnet-api

dotnet:
	dotnet restore
	dotnet build --no-restore
	dotnet test --no-build --verbosity normal

dotnet-app:
	dotnet run --project ${BASEDIR}/src/app/app.csproj

dotnet-api:
	dotnet run --project ${BASEDIR}/src/api/api.csproj
