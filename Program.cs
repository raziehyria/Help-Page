using Microsoft.EntityFrameworkCore;
using UserHelpPageTemplate.Business;
using DataLayer;
using Business;
using AutoMapper;
using UserHelpPageTemplate.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Serilog;
using AppLogger;
using UserHelpPageTemplate.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);

#region DbContexts
var connectionString = builder.Configuration.GetConnectionString("UHPIdentityDbContext") ?? throw new InvalidOperationException("Connection string 'IdentityDbContext' not found.");


builder.Services.AddDbContext<UHPIdentityDbContext>(options => options.UseSqlServer(connectionString));

//old version auto generated
//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UHPIdentityDbContext>();
// adjust this to work with new user  
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UHPIdentityDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddEntityFrameworkSqlServer().AddDbContext<UserHelpPageDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:UserHelpPagDbContextConnection"]);

});
#endregion DbContexts

#region Scoping
// Add services to the container.
// service to add MVC support

//AddScoped: This means that the DI container will create a single instance of the object per HTTP request (in the case of web applications)
//Within the same request, each time the service is requested,the same instance will be returned.
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBiz, Biz>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
#endregion Scoping

#region Logger Services 

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();

builder.Services.AddLogging(x =>
{
    x.ClearProviders();
    x.AddSerilog();
});

//FOR LOGGER
builder.Services.AddScoped<IUserHelpPageLogger, UserHelpPageLogger>();

#endregion

#region MiddleWear
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// add middlewear files
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/app/{catchall}","/App/index");
app.UseAuthorization();

//end point middle wear
app.MapDefaultControllerRoute();
#endregion MiddleWear

app.Run();