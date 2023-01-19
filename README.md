# ASP.NET Core 6 OpenID Connect authentication sample against to Azure AD

## Using packages

- Microsoft.AspNetCore.Authentication.OpenIdConnect
- Microsoft.AspNetCore.Authentication.Cookies
- IdentityModel.OidcClient

## Getting started

- Create a application at Azure Portal
- Create a application secret
- Edit launchSettings.json file as follows

```console
cp ExAzureOidcSample.Web/Properties/launchSettings.sample.json ExAzureOidcSample.Web/Properties/launchSettings.json
vi ExAzureOidcSample.Web/Properties/launchSettings.json
```

Run the project

```console
dotnet run --project ExAzureOidcSample.Web
```
