namespace YakShop.Server.Services
{
    public class TimeLapseSimulationHostedService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(10);
        private readonly ILogger<TimeLapseSimulationHostedService> _logger;
        private readonly IServiceScopeFactory _factory;
        private int _executionCount = 0;
        public bool IsEnabled { get; set; }

        // https://medium.com/medialesson/run-and-manage-periodic-background-tasks-in-asp-net-core-6-with-c-578a31f4b7a3
        // https://github.com/GrillPhil/PeriodicBackgroundTaskSample/blob/main/PeriodicHostedService.cs

        public TimeLapseSimulationHostedService(
            ILogger<TimeLapseSimulationHostedService> logger,
            IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // ExecuteAsync is executed once and we have to take care of a mechanism ourselves that is kept during operation.
            // To do this, we can use a Periodic Timer, which, unlike other timers, does not block resources.
            // But instead, WaitForNextTickAsync provides a mechanism that blocks a task and can thus be used in a While loop.
            using PeriodicTimer timer = new(_period);

            // When ASP.NET Core is intentionally shut down, the background service receives information
            // via the stopping token that it has been canceled.
            // We check the cancellation to avoid blocking the application shutdown.
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    if (IsEnabled)
                    {
                        // We cannot use the default dependency injection behavior, because ExecuteAsync is
                        // a long-running method while the background service is running.
                        // To prevent open resources and instances, only create the services and other references on a run

                        await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                        DailyHerdStatsUpdateService dailyUpdateService = asyncScope.ServiceProvider.GetRequiredService<DailyHerdStatsUpdateService>();
                        await dailyUpdateService.RunAsync();

                        _executionCount++;
                        _logger.LogInformation(
                            "Executed TimeLapseSimulationHostedService - Count: {ExecutionCount}", _executionCount);
                    }
                    else
                    {
                        _logger.LogInformation(
                            "Skipped TimeLapseSimulationHostedService");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Failed to execute TimeLapseSimulationHostedService with exception message {Message}.", ex.Message);
                }
            }
        }
    }
}