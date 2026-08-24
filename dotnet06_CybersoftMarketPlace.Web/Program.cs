var builder = WebApplication.CreateBuilder(args);

// DI blazor page service
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddOpenApi();

// DI Local Storage
builder.Services.AddLocalStorageServices();

// DI http client service
builder.Services.AddHttpClient("CybersoftMarketplaceApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5269");
});

// DI Service State management
builder.Services.AddScoped<ProductStateService>();
builder.Services.AddScoped<UserStateService>();

var app = builder.Build();




// Cấu hình routing
app.UseRouting();

// Cấu hình Blazor Hub
app.MapBlazorHub();

// Cấu hình trang chủ mặc định
app.MapFallbackToPage("/_Host");

app.UseStaticFiles();

app.Run();
