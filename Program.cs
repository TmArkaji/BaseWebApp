using BaseWebApplication.Configurations;
using BaseWebApplication.Configurations.Cryptography;
using BaseWebApplication.Data;
using BaseWebApplication.Interfaces;
using BaseWebApplication.Repositories;
using BaseWebApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

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

// builder.Services.AddTransient<IEmailSender>(s => new EmailService("", 0, ""));
builder.Services.AddTransient<IEmailSender, EmailService>();

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
