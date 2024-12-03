using Microsoft.EntityFrameworkCore;
using TRPG.System;

namespace TRPG.Models
{
    /// <summary>
    /// Db Context
    /// </summary>
    public class Db : DbContext
    {
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users => Set<User>();

        /// <summary>
        /// Characters
        /// </summary>
        public DbSet<Character> Characters => Set<Character>();

        /// <summary>
        /// Spells
        /// </summary>
        public DbSet<Spell> Spells => Set<Spell>();

        /// <summary>
        /// Wands
        /// </summary>
        public DbSet<Wand> Wands => Set<Wand>();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options"></param>
        public Db(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CharacterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SpellEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WandEntityTypeConfiguration());
        }
    }
}
