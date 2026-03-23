using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FoodSafety.MVC.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false) 
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// configure 
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("UserName", "Anonymous")
    .Enrich.WithProperty("Application", "FoodSafetyTracker")
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .WriteTo.File("logs/food_safety_log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// set as default
builder.Host.UseSerilog(); 

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        context.Database.EnsureCreated();
        DbSeeder.seedRoles(roleManager, userManager).GetAwaiter().GetResult();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();