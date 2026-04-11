using ElectronNET.API;

using SoundScapeApp.Services;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

if (HybridSupport.IsElectronActive)
{
    var options = new ElectronNET.API.Entities.BrowserWindowOptions
    {
        Width = 1200,
        Height = 800
    };
    await Electron.WindowManager.CreateWindowAsync(options);

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
