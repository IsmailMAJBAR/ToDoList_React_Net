using Microsoft.EntityFrameworkCore;

using todo_list_server.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("TodoDatabase");
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(connectionString)
);


// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
