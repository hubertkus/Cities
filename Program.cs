var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICityRepository, MemoryCityRepository>();

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

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self' https://app.apresly.com https://widget.apresly.com 'sha256-Q3kPPmbnIbVl3ScyrCibRG2XFX05HpG5Jqwdb1D8iGM='; " +
        "connect-src 'self' https://app.apresly.com; " +
        "img-src 'self' data:; " +
        "style-src 'self' 'unsafe-inline'; " +
        "object-src 'none'; " +
        "form-action 'self'; " +
        "frame-ancestors 'none';");
    await next();
});

app.Run();
