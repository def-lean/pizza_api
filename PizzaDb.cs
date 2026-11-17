using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    public class PizzaDb : DbContext
   {
        public PizzaDb(DbContextOptions<PizzaDb> options) : base(options) { }
        public DbSet<Pizza> Pizzas => Set<Pizza>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<Pizza>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Pizza>().HasMany(p => p.Ingredients).WithMany(i => i.Pizzas).UsingEntity(j => j.ToTable("PizzaIngredients"));
        }
    }
}