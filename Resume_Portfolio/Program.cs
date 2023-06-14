using Lucene.Net.Support;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Resume_Portfolio;
using Resume_Portfolio.Data;
using Resume_Portfolio.Interfaces;
using Resume_Portfolio.Repositories;
using Resume_Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure data protection
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("keys"));

builder.Services.AddControllersWithViews();

// Configure the database context
var connectionString = configuration["ConnectionStrings:jobsearchdatabase"];
builder.Services.AddDbContext<JobContext>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.33")));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddLogging();

// Register repositories and services
builder.Services.AddTransient<IJobRepository, JobRepository>();
builder.Services.AddHttpClient<CompanyService>();
builder.Services.AddHttpClient<JobApplicationService>();

// Other services...

// Configuration binding
builder.Services.Configure<AppSettings>(configuration.GetSection("SensitiveConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
