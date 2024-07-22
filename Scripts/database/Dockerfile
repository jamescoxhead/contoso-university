# Custom dockerfile to create and initialize a SQL Server database.
# SQL Server cannot create a database on startup https://github.com/microsoft/mssql-docker/issues/2
# Based on https://stackoverflow.com/questions/69941444/how-to-have-docker-compose-init-a-sql-server-database
ARG BASE_VERSION=2022-latest

FROM mcr.microsoft.com/mssql/server:${BASE_VERSION}
USER root
WORKDIR /source
COPY . /source/
RUN mkdir -p -m 777 /var/opt/mssql/data && chown -R 10001:0 /var/opt/mssql/data
EXPOSE 1433
USER mssql
CMD ["/bin/bash", "entrypoint.sh"]
