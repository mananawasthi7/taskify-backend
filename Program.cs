using Microsoft.EntityFrameworkCore;
using TaskifyApi.Data; // Replace with the actual namespace where TodoContext is defined

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add health checks
builder.Services.AddHealthChecks();

// Use SQLite instead of in-memory
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite("Data Source=taskify.db"));

// Allow frontend CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173", 
                "https://taskify-backend-jyld.onrender.com",
                "https://*.netlify.app"  // Allow any Netlify subdomain
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ⭐️ ADD THIS TO USE RENDER'S DYNAMIC PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Ensure database is created and migrated
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
    context.Database.Migrate();
}

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

// Add health check endpoint
app.MapHealthChecks("/health");

app.Run();
