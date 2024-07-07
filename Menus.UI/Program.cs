using Menus.UI.Data;
using Menus.UI.IServices;
using Menus.UI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
// Add services to the container.
builder.Services.AddScoped<IMenuService, MenuServices>();

builder.Services.AddControllersWithViews()
		.AddJsonOptions(o =>
		{
			o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
			o.JsonSerializerOptions.PropertyNamingPolicy = null;
		});

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

app.Run();
