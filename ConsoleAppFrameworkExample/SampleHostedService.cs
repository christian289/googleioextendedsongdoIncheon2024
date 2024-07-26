namespace ConsoleAppFrameworkExample;

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
