using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Repos;
using WebApplication1.Properties;
using Microsoft.OpenApi.Models;
using System.Reflection;
using HeroApi.middleware;
using HeroApi.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;


//for NLog, use the main docs and dont copy try catch from doc(write it by yourself to work normaly)
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    ConfigurationManager configuration = builder.Configuration;
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

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    });


    //first have to install pomelo packages,then scafold.after scafolding with pomelo,it dosent work in API so need to delete pomelos and install microssft Core packages
    //then chenge context and program.cs(DI) to working with microsoft pakcages 
    builder.Services.AddDbContext<testContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("db")));
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));
    builder.Services.AddScoped<IHeroApiService, HeroApiService>();
    builder.Services.AddScoped<IHeroService, HeroService>();
    builder.Services.AddScoped<IHeroRepo, HeroRepo>();
    builder.Services.AddScoped<IMethodResult, MethodResult>();
    builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "redis-container:6379"; // Replace with your Redis server connection string
        /*options.InstanceName = "SampleInstance"; // Replace with a unique instance name*/
    });
    // For Identity
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    // Adding Authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ClockSkew = TimeSpan.Zero,

         ValidAudience = configuration["JWT:ValidAudience"],
         ValidIssuer = configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
     };
 });

    //add DI of NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


    var app = builder.Build();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
       
    }
    else if (app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseHsts();
    app.UseMiddleware<ErrorHandlingMiddleware>();




    app.UseAuthentication();
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
