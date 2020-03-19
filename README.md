# aspnetcore-authorization-request

Demonstrates [How to Send an Authorization Request](https://github.com/mrtillman/counter-culture.secure/wiki/How-To-Send-an-Authorization-Request) using [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2).

---

## Getting Started

Use this project to perform a sanity check for your registered OAuth 2.0 client.

## Prerequisites

When in development mode, be sure to start [counter-culture.api](https://github.com/mrtillman/counter-culture.api) and [counter-culture.secure](https://github.com/mrtillman/counter-culture.secure) on ports `4000` and `5000`, respectively. Also remember to register your app via [counter-culture.dev](https://github.com/mrtillman/counter-culture.dev), which will issue your `CLIENT_ID` and `CLIENT_SECRET`.

Next, find `Presentation/appsettings.demo.json`, rename it to `appsettings.json`, and set your environment variables:

```sh
{
  ...
  "CLIENT_ID": "{CLIENT_ID}",
  "CLIENT_SECRET": "{CLIENT_SECRET}",
  "REDIRECT_URI": "http://counter-culture:8080/oauth2/callback"
}
```

## Installation

```bash
$ dotnet build
```

## Launching the app

```bash
$ dotnet run -p Presentation/Presentation.csproj
```

## Tests

```bash
# unit tests
$ dotnet test Tests/Tests.csproj

# acceptance tests
$ dotnet test Specification/Specification.csproj
```

## Usage

Open http://counter-culture:8080 to view it in the browser. You can sign in using one of the [demo accounts](https://github.com/mrtillman/counter-culture.secure/blob/master/README.md#usage).

To exercise the token refresh logic, visit http://counter-culture:8080/renewtoken.

## License

[MIT](https://github.com/mrtillman/aspnetcore-authorization-request/blob/master/LICENSE)
