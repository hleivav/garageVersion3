using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GarageVersion3.Web.Data;
using GarageVersion3.Data;
using GarageVersion3.Web.Automapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GarageVersion3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GarageVersion3Context") ?? throw new InvalidOperationException("Connection string 'GarageVersion3Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    
    var db = scope.ServiceProvider.GetRequiredService<GarageVersion3Context>();

    //db.Database.EnsureDeleted(); //avmarkera när man inte behöver befolka db:n
    //db.Database.Migrate();

    try
    {
        await SeedData.InitAsync(db);
    }
    catch (Exception e){
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(string.Join(" ", e.Message));
    }
}

//SeedData


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
    pattern: "{controller=Members}/{action=Index}/{id?}");

app.Run();
