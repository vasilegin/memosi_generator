using MemesApi.Db;
using Microsoft.EntityFrameworkCore;

namespace MemesApi.Starter
{
    public class MigrationStarter : IHostedService
    {

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MigrationStarter(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var score = _serviceScopeFactory.CreateScope();
            var memeContext = score.ServiceProvider.GetService<MemeContext>();

            if (memeContext is null) return;
            
            await memeContext.Database.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
