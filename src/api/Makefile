-include ../../.env

all:
	export SQL_CONNECTION="Server=localhost;UID=sa;PWD=${MSSQL_SA_PASSWORD};Database=demo;trusted_connection=false;Persist Security Info=False;Encrypt=False;"
	ls -A ./Migrations| grep -v .keep || dotnet ef migrations add InitialCreate
	dotnet ef database update

install:
	dotnet tool update --global dotnet-ef

clean:
	dotnet ef database drop -f || true
	rm -rf Migrations/*.cs obj/* bin/*
	dotnet clean
