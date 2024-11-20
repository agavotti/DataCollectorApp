using WebFormsApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios para controladores con vistas y HttpClient
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ApiService>();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Albums}/{action=Index}/{id?}");



app.Run();