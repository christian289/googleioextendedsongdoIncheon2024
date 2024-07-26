namespace ConsoleAppFrameworkExample;

// 이 서비스는 Generic Host에서 실행되는 Hosted Service로서, 주기적인 작업을 수행합니다.
// ConsoleAppFramework는 자체적인 lifetime을 가지고 있기 때문에, Hosted Service를 사용할 수 없습니다.
internal class SampleHostedService(ILogger<SampleHostedService> logger) : IHostedService
{
    private readonly ILogger<SampleHostedService> logger = logger;
    private readonly PeriodicTimer timer = new(TimeSpan.FromSeconds(2));

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.ZLogInformation($"SampleHostedService Start");

        Task.Factory.StartNew(
                creationOptions: TaskCreationOptions.LongRunning,
                cancellationToken: cancellationToken,
                scheduler: TaskScheduler.Default,
                action: async () =>
                {
                    SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

                    while (await timer.WaitForNextTickAsync())
                        logger.ZLogInformation($"Hello I/O Extended 2024!!! 현재 시간은...! {DateTime.Now:yyyy:MM:dd hh:mm:ss}");
                }
                );

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.ZLogInformation($"SampleHostedService Stop");

        return Task.CompletedTask;
    }
}
