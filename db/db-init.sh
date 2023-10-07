#!/usr/bin/env bash

#run the setup script to create the DB and the schema in the DB
#do this in a loop because the timing for when the SQL instance is ready is indeterminate
for i in {1..50};
do
  echo "Creating database...."
  #run the setup script to create the DB, table, index, sproc, and populate data
  /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -d master -i db-init.sql
  if [ $? -eq 0 ]
  then
      echo "db-init.sql completed"
      break
  else
      echo "Wait for SQL Server to start..."
      sleep 1
  fi
done

