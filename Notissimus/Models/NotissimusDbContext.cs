using Microsoft.EntityFrameworkCore;

namespace Notissimus.Models
{
    public class NotissimusDbContext : DbContext
    {
        public NotissimusDbContext(DbContextOptions<NotissimusDbContext> options)
            : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; } = null!;
    }
}
