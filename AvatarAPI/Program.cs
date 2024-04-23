using AvatarAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IImageContext>(c => new ImageContext("Db\\data.db"));
builder.Services.AddScoped<IAvatarService, AvatarService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("All", policy => { policy.AllowAnyOrigin(); });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("All");

app.UseAuthorization();

app.MapControllers();

app.Run();
