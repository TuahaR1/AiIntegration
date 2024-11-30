using GroqSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var apiKey = builder.Configuration["ApiKey"];
var apiModel = "llama-3.1-70b-versatile";

// Register the GroqClient as a singleton using a factory method
builder.Services.AddSingleton<IGroqClient>(sp =>
    new GroqClient(apiKey, apiModel)
        .SetTemperature(0.5)
        .SetMaxTokens(512)
        .SetTopP(1)
        .SetStop("NONE")
        .SetStructuredRetryPolicy(5));

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
