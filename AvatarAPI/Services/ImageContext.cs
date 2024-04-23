using Microsoft.EntityFrameworkCore;

namespace AvatarAPI.Services
{
    public class ImageContext : DbContext, IImageContext
    {
        public DbSet<Image> Images { get; set; }

        string _dbPath;

        public ImageContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }

    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
