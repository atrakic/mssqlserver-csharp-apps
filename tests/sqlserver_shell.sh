#!/usr/bin/env bash
DB="${1}"
docker exec -it "$DB" /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U SA -P 'Strong=Passw0rd'
