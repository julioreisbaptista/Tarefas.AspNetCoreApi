using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Tarefas.AMQP.Servicos;
using Tarefas.AspNetCoreApi;
using Tarefas.AspNetCoreApi.Services;
using Tarefas.Salvar.DataBase.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Host.ConfigureServices(s => {

    s.AddTransient<IRabbitMQService, RabbitMQService>();
    s.AddTransient<ITarefaRepositorio, TarefaRepositorio>();
    s.AddTransient(_ => new MySqlConnection(config["ConnectionStrings:Default"]));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

app.Run();
