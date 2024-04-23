using Microsoft.EntityFrameworkCore;

namespace AvatarAPI.Services
{
    public interface IImageContext
    {
        DbSet<Image> Images { get; set; }
    }
}