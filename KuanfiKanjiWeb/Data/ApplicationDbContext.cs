using KuanfiKanjiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace KuanfiKanjiWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Kanji> Kanji { get; set; }
        public DbSet<RelatedWords> RelatedWords { get; set; }
        public DbSet<KanjiSet> KanjiSet { get; set; }
    }
}
