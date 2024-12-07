using Microsoft.EntityFrameworkCore;
using Universidad.Data;
using Microsoft.OpenApi.Models; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<UniversidadContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()); 


builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient("MyHttpClient");


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Universidad",
        Version = "v1",
        Description = "API para gestionar la Universidad",
        Contact = new OpenApiContact
        {
            Name = "Juan Daniel Luevano Ruiz",
            Email = "juandaniel@gmail.com" // 
        }
    });
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); 
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Universidad V1");
    c.RoutePrefix = ""; 
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "alumnosView",
    pattern: "AlumnosView/{action=Card}/{id?}",
    defaults: new { controller = "AlumnosView", action = "Card" });

app.MapControllerRoute(
    name: "alumnosApi",
    pattern: "api/Alumnos/{action}/{id?}",
    defaults: new { controller = "Alumnos" });

app.MapControllerRoute(
    name: "alumnosDelete",
    pattern: "AlumnosView/Delete/{id}",
    defaults: new { controller = "AlumnosView", action = "Delete" });


app.Run();
