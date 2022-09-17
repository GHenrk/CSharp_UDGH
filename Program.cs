using System.Globalization;

using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CSharpUdemy_MVC.Data;
using Pomelo.EntityFrameworkCore.MySql;
using CSharpUdemy_MVC.Services;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CSharpUdemy_MVCContext>(options =>
    options.UseMySql("server=localhost;user=root;password=1234567; database=CSharpUdemy", MySqlServerVersion.Parse("8.0.30-mysql")));

builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();

builder.Services.AddControllersWithViews();

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
SeedDatabase();
LocalizationService();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

void SeedDatabase() //Cria uma função
{
    using (var scope = app.Services.CreateScope()) //Usando o escopo criado ele inicializa o servico SeedingService
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<SeedingService>();
        dbInitializer.Seed(); //Roda a funcao do seedingService
    }
}

void LocalizationService()
{
   
        var enUs = new CultureInfo("en-US");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(enUs),
            SupportedCultures = new List<CultureInfo> { enUs },
            SupportedUICultures = new List<CultureInfo> { enUs },
        };
        app.UseRequestLocalization(localizationOptions);
    
}