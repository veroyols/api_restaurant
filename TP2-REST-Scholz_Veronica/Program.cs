using Application.Interfaces;
using Application.UseCase;
using Infrastructure.cqrs_Command;
using Infrastructure.cqrs_Query;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

//CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3001", "http://172.17.208.1:3001")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// INYECCION POR DEPENDENCIAS//ver timelife
builder.Services.AddScoped<ICommandComanda, CommandComanda>();
builder.Services.AddScoped<ICommandMercaderia, CommandMercaderia>();
builder.Services.AddScoped<IQueryComanda, QueryComanda>();
builder.Services.AddScoped<IQueryComandaMercaderia, QueryComandaMercaderia>();
builder.Services.AddScoped<IQueryFormaEntrega, QueryFormaEntrega>();
builder.Services.AddScoped<IQueryMercaderia, QueryMercaderia>();
builder.Services.AddScoped<IQueryTipoMercaderia, QueryTipoMercaderia>();
builder.Services.AddScoped<IServiceComanda, ServiceComanda>();
builder.Services.AddScoped<IServiceMercaderia, ServiceMercaderia>();

//CONECTION STRING
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(connectionString));

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

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
