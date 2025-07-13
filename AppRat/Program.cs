using AppRat.Controllers;
using AppRat.Data;
using AppRat.Services;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using static AppRat.Areas.Identity.Pages.Account.ForgotPasswordModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add DbContexts
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AppRatDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
});

// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false) //to togle email confirmation
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Shared/{0}.cshtml");
    });

builder.Services.AddScoped<IDropdownService, DropdownService>();
builder.Services.AddScoped<IApplicationsService, ApplicationsService>();

// Add Sidebar menu json file
builder.Configuration.AddJsonFile("sidebar.json", optional: true, reloadOnChange: true);
//builder.Configuration.AddJsonFile("adminsidebar.json", optional: true, reloadOnChange: true);

// Add EmailSender service
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddTransient<MailerController>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create roles if they don't exist
    var roles = new[] { "Admin", "User", "Developer", "Guest" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseCors(); // Place the CORS middleware after UseAuthorization and before MapControllerRoute

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.MapRazorPages();

app.Run();

//builder.Services.AddCors(options =>
//{   
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.ExpireTimeSpan = TimeSpan.FromDays(30); // Adjust as needed
//    options.SlidingExpiration = true; // Extends the expiration time on each request
//});
