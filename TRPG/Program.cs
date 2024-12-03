using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TRPG.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Ma custom API",
            Version = "v1",
        });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "doc.xml");
    x.IncludeXmlComments(filePath);
});
builder.Services.AddDbContext<Db>(x => x.UseSqlite("Data Source=db.db;"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");
app.Run();
