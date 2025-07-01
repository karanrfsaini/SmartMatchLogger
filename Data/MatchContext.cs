using Microsoft.EntityFrameworkCore;
using SmartMatchLogger.Models;

namespace SmartMatchLogger.Data
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options) : base(options)
        {
        }

        public DbSet<Match> Match { get; set; }
    }
}
