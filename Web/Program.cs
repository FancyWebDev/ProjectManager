using System.Text.Json.Serialization;
using BLL.Services;
using DAL.Data;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ICollaboratorService, CollaboratorService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services
    .AddIdentity<Collaborator, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services
    .AddMvc()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 
builder.Services.AddCors();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

async void SeedData(IHost host)
{
    var scopedFactory = host.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory?.CreateScope();
    var service = scope?.ServiceProvider.GetService<Seed>();
    // service?.SeedData();
    await service?.SeedUsersAndRolesAsync(app);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseCors(options =>
    options.WithOrigins("http://localhost:8080")
        .AllowAnyHeader()
        .AllowAnyMethod());

app.MapControllers();

app.Run();