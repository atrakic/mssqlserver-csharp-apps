ARG MSSQL_VERSION=2022
FROM mcr.microsoft.com/mssql/server:$MSSQL_VERSION-latest
COPY --chown=10001 . /
ENTRYPOINT ["./entrypoint.sh"]
