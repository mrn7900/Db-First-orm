using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Repos;
using WebApplication1.Properties;
using Microsoft.OpenApi.Models;
using System.Reflection;


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
    //config swagger description 
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Hero API",
            Description = "An ASP.NET Core Web API that works with an incoming API that provide data about heros. this API can use for providing data about heros for library apps.",
      /*      TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Example Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            }*/
        });
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
   
    //first have to install pomelo packages,then scafold.after scafolding with pomelo,it dosent work in API so need to delete pomelos and install microssft Core packages
    //then chenge context and program.cs(DI) to working with microsoft pakcages 
    builder.Services.AddDbContext<testContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("db")));
    builder.Services.AddScoped<IHeroApiService, HeroApiService>();
    builder.Services.AddScoped<IHeroService, HeroService>();
    builder.Services.AddScoped<IHeroRepo, HeroRepo>();
    builder.Services.AddScoped<IMethodResult, MethodResult>();
    builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost:6379"; // Replace with your Redis server connection string
        /*options.InstanceName = "SampleInstance"; // Replace with a unique instance name*/
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
    }else if (app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    /*app.UseAuthorization();*/

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
