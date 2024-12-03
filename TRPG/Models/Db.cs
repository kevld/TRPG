using Microsoft.EntityFrameworkCore;
using TRPG.System;

namespace TRPG.Models
{
    public class Db : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Spell> Spells => Set<Spell>();
        public DbSet<Wand> Wands => Set<Wand>();

        public Db(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CharacterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SpellEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WandEntityTypeConfiguration());
        }
    }
}
