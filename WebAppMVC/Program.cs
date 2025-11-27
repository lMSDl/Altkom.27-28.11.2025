using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();


app.MapStaticAssets(); //endpoint

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
