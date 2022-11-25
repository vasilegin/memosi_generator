using Prometheus.DotNetRuntime;

namespace MemesApi.Starter
{
	public class RuntimeMetricsStarter: IHostedService
	{

		private IDisposable? _metricsCollector;
		public Task StartAsync(CancellationToken cancellationToken)
		{
			_metricsCollector = DotNetRuntimeStatsBuilder.Customize()
				.WithContentionStats()
				.WithThreadPoolStats()
				.WithExceptionStats()
				.WithGcStats()
				.StartCollecting();

			return Task.CompletedTask;
		}
		public Task StopAsync(CancellationToken cancellationToken)
		{
			_metricsCollector?.Dispose();
			return Task.CompletedTask;
		}
	}
}