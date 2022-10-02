using EmployeeWebApi.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/*Database Context Dependency Injection*/
var dbHost = Environment.GetEnvironmentVariable("DB_HOST"); //"DESKTOP-IFFNHN9\\SQLEXPRESS";
var dbName = Environment.GetEnvironmentVariable("DB_Name");
var dbLogin = Environment.GetEnvironmentVariable("DB_Login");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString_ = $"Data Source={dbHost};Initial Catalog={dbName};User ID={dbLogin};Password={dbPassword}";

builder.Services.AddDbContext<EmployeeDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string not found.")));


// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
