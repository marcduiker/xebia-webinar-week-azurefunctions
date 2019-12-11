# How to use dependency injection in .NET Azure Functions 

This repository contains several branches which can be used to illustrate what needs to be refactored in order to use dependency injection in a .NET Core Azure Functions project.

This repository is used at a live webinar during [Xebia Webinar Week](https://pages.xebia.com/academy-webinar-week/dependency-injection-in-net-azure-functions).

This is the `development-v3-start` branch which contains the initial solution, based on .NET Core 3. Use the refactoring instructions below in order to have the Function App use dependency injection.

|Branch name| Versions | Uses DI |
|-|-|-|
| development-v2-start | .NET Core 2 / Azure Functions 2 | No
| development-v2 | .NET Core 2 / Azure Functions 2 | Yes
| development-v3-start | .NET Core 3 / Azure Functions 3 | No
| development-v3 | .NET Core 3 / Azure Functions 3 | Yes

## Refactoring for .NET Core 3

Ensure that the Azure Function App csproj file is set to the proper .NET Core and Azure Functions runtime versions:

```xml
<TargetFramework>netcoreapp3.1</TargetFramework>
<AzureFunctionsVersion>v3</AzureFunctionsVersion>
```

### Step 1: NuGet Packages

- Add package `Microsoft.Azure.Functions.Extensions` v1.0.0
- Add package `Microsoft.Extensions.Http` v3.1.0

### Step 2: Update constructor of StarWarsMovieProvider

```csharp
private readonly HttpClient _httpClient;

public StarWarsMovieProvider(IHttpClientFactory httpClientFactory)
{
    _httpClient = httpClientFactory.CreateClient();
}
```

### Step 3: Create a IMovieProvider interface

```csharp
public interface IMovieProvider
{
    Task<List<Movie>> GetMovies();
}
```

And let the StarWarsMovieProvider inherit from this interface.

### Step 4: Use the IMovieProvider in the GetMoviesHttp function

``` csharp
private readonly IMovieProvider _movieProvider;

public GetMoviesHttp(IMovieProvider movieProvider)
{
    _movieProvider = movieProvider;
}
```

### Step 5: Add a Statup class

```csharp
[assembly: FunctionsStartup(typeof(Startup))]
namespace Xebia.WebinarWeek
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IMovieProvider, StarWarsMovieProvider>();
        }
    }
}
```

### Step 6: In case of serialization issues

Add the following setting to the App Settings:

`"FUNCTIONS_V2_COMPATIBILITY_MODE": true`