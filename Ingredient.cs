namespace PizzaStore.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }
}