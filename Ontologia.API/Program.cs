using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Persistence.Repositories;
using Ontologia.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options => options.AddDefaultPolicy(
    b => b.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .DisallowCredentials()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Dependency Injection Configuration
// Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserConceptRepository, UserConceptRepository>();
builder.Services.AddScoped<IUserSuggestionRepository, UserSuggestionRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserConceptService, UserConceptService>();
builder.Services.AddScoped<IUserSuggestionService, UserSuggestionService>();

//Endpoinst case conventions configurations
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//startup automapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// documentation setup
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ontologia.API", Version = "v1" });
    options.EnableAnnotations();
});


var app = builder.Build();

// Program
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}


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
