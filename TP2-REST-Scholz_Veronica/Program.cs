using Application.Interfaces;
using Application.UseCase;
using Infrastructure.cqrs_Command;
using Infrastructure.cqrs_Query;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// INYECCION POR DEPENDENCIAS//ver timelife
builder.Services.AddScoped<IQueryComanda, QueryComanda>();
builder.Services.AddScoped<IQueryMercaderia, QueryMercaderia>();
builder.Services.AddScoped<IQueryTipoMercaderia, QueryTipoMercaderia>();
builder.Services.AddScoped<ICommandComanda, CommandComanda>(); 
builder.Services.AddScoped<ICommandComandaMercaderia, CommandComandaMercaderia>();
builder.Services.AddScoped<IServiceComanda, ServiceComanda>();
builder.Services.AddScoped<IServiceComandaMercaderia, ServiceComandaMercaderia>();
builder.Services.AddScoped<IServiceMercaderia, ServiceMercaderia>();
builder.Services.AddScoped<IServiceTipoMercaderia, ServiceTipoMercaderia>();

//CONECTION STRING
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(connectionString));

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
