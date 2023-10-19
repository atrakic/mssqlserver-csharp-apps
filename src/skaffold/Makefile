-include ../../.env

SQL_CONNECTION="Server=localhost;UID=sa;PWD=${MSSQL_SA_PASSWORD};Database=demo;trusted_connection=false;Persist Security Info=False;Encrypt=False;"
all:
	dotnet ef dbcontext scaffold $(SQL_CONNECTION) \
		Microsoft.EntityFrameworkCore.SqlServer \
		--context-dir Data \
		--output-dir Models \
		--data-annotations \
		--force
	dotnet run

install:
	dotnet tool update --global dotnet-ef

clean:
	dotnet ef database drop -f || true
	rm -rf Migrations/*.cs obj/* bin/*
	dotnet clean
