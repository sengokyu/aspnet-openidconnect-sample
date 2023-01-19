using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(options => { })
.AddOpenIdConnect(options =>
{
    var section = builder.Configuration.GetSection("AzureAd");
    var tenantId = section.GetValue<string>("TenantId");

    options.Authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";
    options.MetadataAddress = $"https://login.microsoftonline.com/{tenantId}/v2.0/.well-known/openid-configuration";
    options.ClientId = section.GetValue<string>("ClientId");
    options.ClientSecret = section.GetValue<string>("ClientSecret");
    options.CallbackPath = section.GetValue<string>("CallbackPath");
    options.ResponseType = "code";
    options.ResponseMode = "query";
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("email");
    options.Scope.Add("profile");
    options.Scope.Add(".default");
    options.SaveTokens = true;

    options.Events.OnAuthenticationFailed = (context) =>
    {
        // 認証失敗時の簡易処理
        context.HandleResponse();
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/plain";
        return context.Response.WriteAsync(context.Exception.ToString());
    };
});

builder.Services.AddAuthorization();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
