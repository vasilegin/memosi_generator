namespace MemesApi
{
    public class AppSettings
    {
        public string UrlPrefix { get; set; } = null!;
        /// <summary>
        /// Макс. размер изображения в байтах.
        /// </summary>
        public long MaxImageSize { get; set; }
    }
}
