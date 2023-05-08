using Microsoft.EntityFrameworkCore;
using NoteApi;
using NoteApi.DataStores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<INoteDictionary, EFNoteDictionary>();
builder.Services.AddDbContext<AppDbContext>(
    ( options) =>
        options.UseMySql(
            builder.Configuration.GetConnectionString("Default")!,
            MariaDbServerVersion.LatestSupportedServerVersion
        )
);

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