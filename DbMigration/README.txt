
To run migration
#Open a command line, go to the project folder, and run
dotnet restore

#If everything is fine, you should be able to run
dotnet ef

#After that you can run commands like:
dotnet ef migrations add initial
dotnet ef database update

dotnet ef migrations add MyMigrationName --startup-project DbMigration --project DataAccessEF
dotnet ef database update --startup-project DbMigration --project DataAccessEF

dotnet ef migrations add MyMigrationName --startup-project DbMigration --project DataAccessEFHistory
dotnet ef database update --startup-project DbMigration --project DataAccessEFHistory