#!/usr/bin/env bash
set -eo pipefail

DB="${1}"
PASS="${2}"

shift 2
QUERY="${*}"

declare -a FLAGS
FLAGS=()

if [[ -n "$QUERY" ]]; then FLAGS+=( -Q "${QUERY}" -b ); fi
docker exec -it "$DB" /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${PASS}" "${FLAGS[@]}"
