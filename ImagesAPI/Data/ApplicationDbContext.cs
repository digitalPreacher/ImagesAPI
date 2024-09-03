using ImagesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImagesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ImageModel> Images { get; set; }
    }
}
