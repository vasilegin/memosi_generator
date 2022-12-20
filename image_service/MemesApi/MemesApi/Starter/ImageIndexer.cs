using MemesApi.Db;
using MemesApi.Db.Models;

namespace MemesApi.Starter
{
    public class ImageIndexer : IHostedService
    {

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ImageIndexer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var score = _serviceScopeFactory.CreateScope();
            var memeContext = score.ServiceProvider.GetService<MemeContext>();
            
            if (memeContext is null) throw new ApplicationException("Can't get MemeContext service");
            
            if(!memeContext.Files.Any())
            {
                var directoryPath = Path.Combine(Environment.CurrentDirectory, "static");
                
                var (files, metas) = Directory.EnumerateFiles(directoryPath)
                    .Where(f => !f.Contains(".gitkeep"))
                    .Select(path =>
                    {
                        FileSystemInfo info = new FileInfo(path);
                        var meta = new FileMeta
                        {
                            Format = info.Extension.Remove(0), //  Extension возвращает расширение с точкой, нам оно не нужно
                            CreationDate = info.CreationTime,
                            UpdateDate = info.LastWriteTime
                        };
                        var file = new MemeFile
                        {
                            FileName = path.Split(Path.DirectorySeparatorChar).Last(),
                            Meta = meta,
                        };

                        return (file, meta);
                    })
                    .Aggregate((new List<MemeFile>(), new List<FileMeta>()), (unpacked, record) =>
                    {
                        unpacked.Item1.Add(record.file);
                        unpacked.Item2.Add(record.meta);
                        return unpacked;
                    });

                await memeContext.Metas.AddRangeAsync(metas);
                await memeContext.Files.AddRangeAsync(files);
                await memeContext.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
