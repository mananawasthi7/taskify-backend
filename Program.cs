using Microsoft.EntityFrameworkCore;
using TaskifyApi.Data; // Replace with the actual namespace where TodoContext is defined

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use SQLite instead of in-memory
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite("Data Source=taskify.db"));

// Allow frontend CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://eclectic-mousse-ea4fca.netlify.app")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ⭐️ ADD THIS TO USE RENDER'S DYNAMIC PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Dev middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowVueApp");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
