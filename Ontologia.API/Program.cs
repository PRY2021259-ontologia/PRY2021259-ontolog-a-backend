using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ontologia.API.Domain.Persistence.Contexts;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Extensions;
using Ontologia.API.Persistence.Repositories;
using Ontologia.API.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(
    options => options.AddDefaultPolicy(b => 
        b.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .DisallowCredentials()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlAzureConnection"));
});


// Dependency Injection Configuration
// Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserConceptRepository, UserConceptRepository>();
builder.Services.AddScoped<IUserSuggestionRepository, UserSuggestionRepository>();
builder.Services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();
builder.Services.AddScoped<IConceptTypeRepository, ConceptTypeRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<ISuggestionTypeRepository, SuggestionTypeRepository>();
builder.Services.AddScoped<ICategoryDiseaseRepository, CategoryDiseaseRepository>();
builder.Services.AddScoped<IPlantDiseaseRepository, PlantDiseaseRepository>();
builder.Services.AddScoped<IUserConceptPlantDiseaseRepository, UserConceptPlantDiseaseRepository>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<ISuggestionStatusRepository, SuggestionStatusRepository>();
builder.Services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();

// Services
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserConceptService, UserConceptService>();
builder.Services.AddScoped<IUserSuggestionService, UserSuggestionService>();
builder.Services.AddScoped<IUserHistoryService, UserHistoryService>();
builder.Services.AddScoped<IConceptTypeService, ConceptTypeService>();
builder.Services.AddScoped<IUserTypeService, UserTypeService>();
builder.Services.AddScoped<ISuggestionTypeService, SuggestionTypeService>();
builder.Services.AddScoped<ICategoryDiseaseService, CategoryDiseaseService>();
builder.Services.AddScoped<IPlantDiseaseService, PlantDiseaseService>();
builder.Services.AddScoped<IUserConceptPlantDiseaseService, UserConceptPlantDiseaseService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<ISuggestionStatusService, SuggestionStatusService>();
builder.Services.AddScoped<IStatusTypeService, StatusTypeService>();

builder.Services.AddScoped<IRestClientExtensions, RestClientExtensions>();

//Endpoinst case conventions configurations
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//startup automapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

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
    context?.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors();
//
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/",  context => { 
        context.Response.Redirect("/swagger/index.html", false);
        return Task.FromResult(0);
    });
    endpoints.MapControllers();
});
app.MapControllers();
app.Run();
