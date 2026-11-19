using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models 
{
    public class Pizza
    {
          public int Id { get; set; }
          [Required, MaxLength(30)]
          public string Name { get; set; } = string.Empty;
          [Required, MaxLength(150)]
          public string? Description { get; set; }
          [Required, Range(1500, 100000)]
          public int Price { get; set; }
          public bool IsAvailable { get; set; }

          public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    }
}