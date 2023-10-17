#!/usr/bin/env bash
set -eo pipefail

DB="${1}"
PASS="${2}"
QUERY="${3:-SELECT @@VERSION}"

docker exec -it "$DB" /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${PASS}" -Q "${QUERY}" -b
