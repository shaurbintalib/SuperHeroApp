using Microsoft.EntityFrameworkCore;
using SuperHeroApp.Entities;

namespace SuperHeroApp.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
