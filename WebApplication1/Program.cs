using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Repos;
using WebApplication1.Properties;


//for NLog, use the main docs and dont copy try catch from doc(write it by yourself to work normaly)
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddHttpClient();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    //builder.Services.AddTransient<MySqlConnection>(_ =>
    //  new MySqlConnection("Server=127.0.0.1;Port=3306;Database=test;Uid=root;Pwd=12345678;"));

    //change packages to danial project
    //first have to install pomelo packages,then scafold.after scafolding with pomelo,it dosent work in API so need to delete pomelos and install microssft Core packages
    //then chenge context and program.cs(DI) to working with microsoft pakcages 
    builder.Services.AddDbContext<testContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("db")));
    builder.Services.AddScoped<IHeroApiService, HeroApiService>();
    builder.Services.AddScoped<IHeroService, HeroService>();
    builder.Services.AddScoped<IHeroRepo, HeroRepo>();
    builder.Services.AddScoped<IMethodResult, MethodResult>();
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost:6379,password=3"; // Replace with your Redis server connection string
        options.InstanceName = "SampleInstance"; // Replace with a unique instance name
    });

    //add DI of NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex);
    throw(ex);
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
