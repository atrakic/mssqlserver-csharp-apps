ARG MSSQL_VERSION=2022

FROM mcr.microsoft.com/mssql/server:$MSSQL_VERSION-latest
ENV PATH="/opt/mssql-tools/bin:${PATH}"
ENV ACCEPT_EULA=Y

COPY --chown=10001 . /
ENTRYPOINT ["./entrypoint.sh"]
