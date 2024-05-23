using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Fans.Data;

var builder = WebApplication.CreateBuilder(args);
/* builder.Services.AddDbContext<PostsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PostsContext") ?? throw new InvalidOperationException("Connection string 'PostsContext' not found.")));
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("UserContext") ?? throw new InvalidOperationException("Connection string 'UserContext' not found.")));
 */

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));
 
 // Add services to the container.
builder.Services.AddControllersWithViews();
// add memory support
builder.Services.AddDistributedMemoryCache();
// add session support?
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    // Create db
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    context.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();
app.UseStaticFiles();
app.UseSession();

app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "Login",
    pattern: "{controller=Auth}/{action=login}/"
);

app.MapControllerRoute(
    name: "Register",
    pattern: "{controller=Auth}/{action=Register}/"
);

app.Run();
