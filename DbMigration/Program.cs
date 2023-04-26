// See https://aka.ms/new-console-template for more information
using DataAccessEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services;

var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
var connectionString = config.GetConnectionString("Connection");
var contextOptions = new DbContextOptionsBuilder<InOutManagementContext>()
    .UseSqlServer(connectionString)
    .Options;

try
{
    using (var context = new InOutManagementContext(contextOptions))
    {
        context.Database.Migrate();

        var databaseService = new DatabaseService(context);
        databaseService.CreateDefaultData();

        Console.WriteLine("Done migration with default user admin");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error to migrate db: " + ex.Message);
}