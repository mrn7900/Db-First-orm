using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection("Server=127.0.0.1;Port=3306;Database=test;Uid=root;Pwd=12345678;"));*/

//change packages to danial project

builder.Services.AddDbContext<testContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("db")));



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
