# Services needed to support Strainth backend

## Sql Server Docker Compose
Creates a container running SQL Server via Docker Compose

### Files and content needed to run SQL Server in docker
- Create a `sapassword.env` file and enter something similar to: 
    - `MSSQL_SA_PASSWORD=MY_SA_PASSWORD`
- Create a `sqlserver.env` file and enter something similar to: 
    - `ACCEPT_EULA=Y`
    - `MSSQL_DATA_DIR=/var/opt/sqlserver/data`
    - `MSSQL_LOG_DIR=/var/opt/sqlserver/log`
    - `MSSQL_BACKUP_DIR=/var/opt/sqlserver/backup`
- `docker-compose.yml` uses a local `dockerfile` to create a customized SQL Server image

## Seq logging

docker-compose up -d
