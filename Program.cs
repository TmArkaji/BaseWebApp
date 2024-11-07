using BaseWebApplication.Configurations;
using BaseWebApplication.Configurations.Cryptography;
using BaseWebApplication.Configurations.ExceptionsHandler;
using BaseWebApplication.Data;
using BaseWebApplication.Interfaces;
using BaseWebApplication.Repositories;
using BaseWebApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, IEmailService>();

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IDummyClassRepository, DummyClassRepository>();
builder.Services.AddScoped<IDummyClassTypeRepository, DummyClassTypeRepository>();
builder.Services.AddScoped<IAppUserConfigRepository, AppUserConfigRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));


#region Localization 
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("es"),
        new CultureInfo("en")
    };

    options.DefaultRequestCulture = new RequestCulture("es");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

});
#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

#region Cryptography 
builder.Services.AddScoped<ICryptoParamsProtector, CryptoParamsProtector>();
builder.Services.AddScoped(typeof(CryptoParamsProtector));
builder.Services.AddMvc(mvcOptions =>
{
    mvcOptions.ValueProviderFactories.Add(new CryptoValueProviderFactory());
});
#endregion

#region Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
#endregion

var app = builder.Build();

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRequestLocalization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
