using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));


builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();