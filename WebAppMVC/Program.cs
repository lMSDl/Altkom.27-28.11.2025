using Bogus;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.InMemory;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

//rejestracja serwisu z ró¿nymi czasami ¿ycia

//transient - nowa instancja za ka¿dym razem gdy jest wstrzykiwana
//builder.Services.AddTransient<IProductsService, ProductsService>();

//scoped - jedna instancja na czas trwania ¿¹dania HTTP
//builder.Services.AddScoped<IProductsService, ProductsService>();

//singleton - jedna instancja na ca³y czas dzia³ania aplikacji
builder.Services.AddSingleton<IProductsService, ProductsService>();

builder.Services.AddSingleton(new Product { Name = "", Price = 44 });

//builder.Services.AddSingleton<IService<User>, BogusService<User>>();
builder.Services.AddSingleton<IService<User>, UsersBogusService>();
builder.Services.AddTransient<Faker<User>, UserFaker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//middleware w³¹czaj¹cy domyœlne pliki jak index.html
//app.UseDefaultFiles(); 

//middleware w³¹czaj¹cy statyczne pliki z wwwroot
/*app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider( Path.Combine(Directory.GetCurrentDirectory(), "public")),
    RequestPath = "/public", //zmienia domyœln¹ œcie¿kê dostêpu do plików statycznych
    OnPrepareResponse = x => x.Context.Response.Headers.Add("Cache-Control", "public,max-age=60000") //ustawienie nag³ówków odpowiedzi HTTP
});*/

//app.UseDirectoryBrowser(); //przegl¹darka plików w formie html
/*app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "public")),
    RequestPath = "/public", //zmienia domyœln¹ œcie¿kê dostêpu do plików statycznych
});
*/

//³¹czy w sobie trzy powy¿sze middleware i daje mo¿liwoœæ konfiguracji za pomoc¹ pojecynczego obiektu
//app.UseFileServer(new FileServerOptions() { EnableDirectoryBrowsing = true }); 
/*var options = new FileServerOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "public")),
    RequestPath = "/public", //zmienia domyœln¹ œcie¿kê dostêpu do plików statycznych
    EnableDirectoryBrowsing = true,
};
options.StaticFileOptions.OnPrepareResponse = x => x.Context.Response.Headers.Add("Cache-Control", "public,max-age=60000");
app.UseFileServer(options);*/


app.Use(async (context, next) =>
{
    var service = context.RequestServices.GetRequiredService<IProductsService>();
    //coœ wa¿nego robimy z serwisem

    await next(context);
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();


app.MapStaticAssets(); //endpoint

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
