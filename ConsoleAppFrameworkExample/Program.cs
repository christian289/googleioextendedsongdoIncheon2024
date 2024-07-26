using ConsoleAppFrameworkExample;
using ConsoleAppFrameworkExample.Options;

// CreateApplicationBuilder와 CreateDefaultBuilder 중 더 최신 트렌드는 CreateApplicationBuilder 입니다.
// 단순하게 Host Application을 정의하는 코드 형태의 차이만 존재하지만,
// 속성으로 접근하는 것이 Callback 메서드를 등록하여 접근하는 것보다 명확하고 선호되는 방식이기 때문입니다.
// 따라서 가독성을 위해 CreateApplicationBuilder를 사용합니다.
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

#region .NET Generic Host Configuration
builder.Configuration.Sources.Clear();
builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true); // Console App 환경은 ASP.NET Core와 다르게 환경별 설정 파일을 사용하지 않는다.
//builder.Configuration.AddCommandLine(args); // .NET 기본 CommandLine Parameter 방식 사용할 때 추가.
#endregion

#region .NET Option Pattern
var options = builder.Configuration.GetSection(nameof(SampleOption)).Get<SampleOption>()!;
Console.WriteLine("옵션 값 제대로 바인딩 되었는지 체크할 때 사용!!");
Console.WriteLine($"Name: {options.Name}");
Console.WriteLine($"Age: {options.Age}");
Console.WriteLine($"Description: {options.Description}");
#endregion

#region CySharp ZLogger Configuration
builder.Services.AddLogging(x =>
{
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Information);
    //x.AddConsole(); // Micsosoft.Extensions.Logging
    x.AddZLoggerConsole(); // ZLogger
});
#endregion

#region Regist Services
//https://learn.microsoft.com/ko-kr/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-8.0&tabs=visual-studio
builder.Services.AddHostedService<SampleHostedService>();
#endregion

// Build
using IHost host = builder.Build();

#region Start Application
//이것을 사용할 경우 Generic Host로 동작
//host.Run();
#endregion

#region ConsoleAppFramework
// 이것을 사용할 경우 ConsoleAppFramework로 동작
using var scope = host.Services.CreateScope();
ConsoleApp.ServiceProvider = scope.ServiceProvider;
ConsoleApp.Run(args, ([FromServices] ILogger<Program> logger) => logger.LogInformation("Hello World!"));
#endregion