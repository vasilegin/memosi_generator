namespace MemesApi.Db.Models
{
    public class MemeFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int? MetaId { get; set; }

        //Чтобы можно было строить запросы типа
        // _context.Files.Include(f => f.Meta).Include(f => f.Estimates)
        public FileMeta? Meta { get; set; }

        public List<Estimate> Estimates { get; set; }
    }
}
