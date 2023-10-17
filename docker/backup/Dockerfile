ARG MSSQL_VERSION=2022

FROM mcr.microsoft.com/mssql/server:$MSSQL_VERSION-latest
LABEL maintainer="Admir Trakic <atrakic@users.noreply.github.com>"
WORKDIR /var/opt/mssql/data

# https://github.com/microsoft/sql-server-samples/releases/tag/adventureworks
ARG BACKUP=https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2022.bak
RUN wget -c $BACKUP

CMD ["/opt/mssql/bin/sqlservr"]
