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

var historyConnectionString = config.GetConnectionString("HistoryConnection");
var historyContextOptions = new DbContextOptionsBuilder<HistoryContext>()
    .UseSqlServer(historyConnectionString)
    .Options;

try
{
    var context = new InOutManagementContext(contextOptions);
    Console.WriteLine("DB Migration connect db: " + context.Database.CanConnect());
    context.Database.Migrate();

    var historyContext = new HistoryContext(historyContextOptions);
    Console.WriteLine("History DB Migration connect db: " + historyContext.Database.CanConnect());
    historyContext.Database.Migrate();

    var databaseService = new DatabaseService(context, historyContext);

    if (bool.Parse(config.GetSection("CreateDefaultData").Value))
    {
        databaseService.CreateDefaultData();
    }


    Console.WriteLine("Done migration with default user admin");
    
}
catch (Exception ex)
{
    Console.WriteLine("Error to migrate db: " + ex.Message);
}