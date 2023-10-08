FROM mcr.microsoft.com/mssql/server:2022-latest@sha256:d00c97dedf4f5c785bbbd5894f490d50124ff409d7751fbba2d53eb7de9965da
RUN mkdir -p /var/opt/mssql/data && cd /var/opt/mssql/data && wget -c https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2016_EXT.bak
#COPY AdventureWorks2016_EXT.bak /var/opt/mssql/data/AdventureWorks2016_EXT.bak
CMD ["/opt/mssql/bin/sqlservr"]
