var builder = WebApplication.CreateBuilder(args);

var clientId = "YOUR_CLIENT_ID";
var clientSecret = "YOUR_SECRET";

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect(
   authenticationScheme: "Google",
   displayName: "Google",
   options =>
   {
       options.Authority = "https://accounts.google.com/";
       options.ClientId = clientId;

       // Change the callback path to match the google app configuration
       options.CallbackPath = "/signin-google";

       // Add email scope
       options.Scope.Add("email");
   });

builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
