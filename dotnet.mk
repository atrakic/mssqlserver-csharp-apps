.PHONY: dotnet dotnet-app dotnet-api

dotnet:
	dotnet restore
	dotnet build --no-restore
	dotnet test --no-build --verbosity normal

dotnet-app:
	SQL_CONNECTION='Server=localhost;UID=sa;PWD=${MSSQL_SA_PASSWORD};trusted_connection=false;Persist Security Info=False;Encrypt=False;' \
								 dotnet run --project ${BASEDIR}/src/app/app.csproj

dotnet-api:
	SQL_CONNECTION='Server=localhost;UID=sa;PWD=${MSSQL_SA_PASSWORD};trusted_connection=false;Persist Security Info=False;Encrypt=False;' \
								 dotnet run --project ${BASEDIR}/src/api/api.csproj
