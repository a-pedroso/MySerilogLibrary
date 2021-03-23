## Purpose of this solution:

Create a Serilog common library for my projects

Still making use of appsettings but simpler

```json
  "MySerilogLibrary": {
    "ApplicationName": "my-serilog-library-api",
    "Environment": "Development",
    "MinimumLevel": "Information",
    "LokiUrl": "http://localhost:3100",
    "SeqUrl": "http://localhost:5341"
  }
```

Also, on IHostBuilder (Program.cs), the UseSerilog is needed:
```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
   Host.CreateDefaultBuilder(args)
       .UseSerilog()// wire serilog to aspnet core
       .ConfigureWebHostDefaults(webBuilder =>
       {
           webBuilder.UseStartup<Startup>();
       });
```

Final note, add this line to log all requests made to API on Configure method at Startup.cs

```csharp
app.UseSerilogRequestLogging();
```


## Run Application
Is based on Project Tye. Getting Started [here](https://github.com/dotnet/tye/blob/main/docs/getting_started.md).
<br/>
On Solution Folder, on your shell of your choice type
```shell
tye run
```


## Technologies
* .NET 5.0
* ASP.NET Core 5.0
* Swagger
* Serilog
* Project Tye

## License

This project is licensed with the [MIT license](LICENSE).
