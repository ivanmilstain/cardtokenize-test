using System.Configuration;
using CardTokenizationTest.Database;
using CardTokenizationTest.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static CardTokenizationTest.Models.AppConfigs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.WebHost.UseConfiguration(configBuilder);

var identitySetting = builder.Configuration.GetSection(nameof(IdentityApi));
builder.Services.Configure<IdentityApi>(identitySetting);
var vaultSetting = builder.Configuration.GetSection(nameof(VaultApi));
builder.Services.Configure<VaultApi>(vaultSetting);
var paymentSetting = builder.Configuration.GetSection(nameof(PaymentApi));
builder.Services.Configure<PaymentApi>(paymentSetting);

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IIdentityServices, IdentityServices>();
builder.Services.AddTransient<ICardServices, CardServices>();
builder.Services.AddTransient<IPaymentServices, PaymentServices>();

builder.Services.AddDbContext<TokenizationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

DatabaseManagementService.MigrationInitialisation(app);

app.Run();

