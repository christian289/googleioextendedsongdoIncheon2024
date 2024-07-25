using ConsoleAppFrameworkExample.Options;
using ConsoleAppFrameworkExample.Quartz;

// CreateApplicationBuilder와 CreateDefaultBuilder 중 더 최신 트렌드는 CreateApplicationBuilder 입니다.
// 단순하게 Host Application을 정의하는 코드 형태의 차이만 존재하지만,
// 속성으로 접근하는 것이 Callback 메서드를 등록하여 접근하는 것보다 명확하고 선호되는 방식이기 때문입니다.
// 따라서 가독성을 위해 CreateApplicationBuilder를 사용합니다.
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

#region .NET Generic Host Configuration
builder.Environment.ContentRootPath = AppDomain.CurrentDomain.BaseDirectory;
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddCommandLine(args);
#endregion

#region .NET Option Pattern
SampleOption options = new();
var section = builder.Configuration.GetSection(nameof(SampleOption));
section.Bind(options);

Console.WriteLine("옵션 값 제대로 바인딩 되었는지 체크할 때 사용!!");
Console.WriteLine($"Name: {options.Name}");
Console.WriteLine($"Age: {options.Age}");
Console.WriteLine($"Description: {options.Description}");

builder.Services.Configure<SampleOption>(section);
#endregion

#region Quartz.NET Boilerplate Configuration
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<PrintJob>();
builder.Services.AddSingleton(new JobSchedule(
    jobType: typeof(PrintJob),
    cronExpression: "0/5 * * * * ?")); // cron 식, 5초마다 실행

builder.Services.AddHostedService<QuartzHostedService>();
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