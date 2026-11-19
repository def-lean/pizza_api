namespace PizzaStore.Models
{
    public class PizzaDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }

        public List<IngredientDto> Ingredients { get; set; } = new();
    }
}