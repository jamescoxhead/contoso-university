echo "Waiting 30s for DB server to start..."
sleep 30s
echo "Attempting to create database..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i Create-Database.sql
echo "Database init completed"
