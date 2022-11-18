using Microsoft.EntityFrameworkCore;
using Model.DAL;
using Model.Repositories;
using Zoo.Views.ViewComponents;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ZooDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);
builder.Services.AddScoped<AnimelRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CommentRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooDBContext>();
    //context.Database.EnsureDeleted(); //For ReSeedng the Database
    context.Database.EnsureCreated();
}

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{param?}"
    );
}
);
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Sorry cant serve your request");
});

app.Run();
