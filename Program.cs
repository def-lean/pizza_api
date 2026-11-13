using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddSqlite<PizzaDb>(connectionString);

var app = builder.Build();



app.MapGet("/", () => "Hello World!");
// app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.Select(p => new PizzaDto{ Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price, IsAvailable = p.IsAvailable }).ToListAsync());

app.MapGet("/pizza/{id}", async (PizzaDb db, int id) =>
{
    var pizza = await db.Pizzas.Where(p => p.Id == id).Select(p => new PizzaDto{ Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price, IsAvailable = p.IsAvailable }).FirstOrDefaultAsync();
    return pizza is not null ? Results.Ok(pizza) : Results.NotFound();
});
app.MapGet("/pizzas", async (PizzaDb db, string? search, bool? isAvailable) =>
{
    var pizzas = await db.Pizzas
        .Where(p => string.IsNullOrEmpty(search) || (p.Name != null && p.Name.Contains(search)) || (isAvailable.HasValue && p.IsAvailable == isAvailable.Value))
        .Select(p => new Pizza { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price, IsAvailable = p.IsAvailable })
        .ToListAsync();
    return Results.Ok(pizzas);
});
app.MapGet("/pizza/order", async(PizzaDb db,string? sortBy, string? sortOrder)=>
{
    var query = db.Pizzas.AsQueryable();
    query = sortBy?.ToLower() switch
    {
        "name" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
        "price" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
        _ => query
    };
    var pizzas = await query.ToListAsync();
    return Results.Ok(pizzas);  
});
app.MapPost("/pizza", async (PizzaDb db, PizzaDto pizzaDto) =>
{
    var pizza = new Pizza { Name = pizzaDto.Name, Description = pizzaDto.Description, Price = pizzaDto.Price, IsAvailable = pizzaDto.IsAvailable };
    db.Pizzas.Add(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", new PizzaDto { Id = pizza.Id, Name = pizza.Name, Description = pizza.Description, Price = pizza.Price, IsAvailable = pizza.IsAvailable });
});
app.MapPut("/pizza/{id}", async (PizzaDb db, PizzaDto updatepizza, int id) =>
{
      var pizza = await db.Pizzas.FindAsync(id);
      if (pizza is null) return Results.NotFound();
      pizza.Name = updatepizza.Name;
      pizza.Description = updatepizza.Description;
      pizza.Price = updatepizza.Price;
      pizza.IsAvailable = updatepizza.IsAvailable;
      await db.SaveChangesAsync();
      return Results.NoContent();
});
app.MapPatch("/pizza/{id}/Availability", async (PizzaDb db, PizzaDto updatepizza, int id) =>
{
      var pizza = await db.Pizzas.FindAsync(id);
      if (pizza is null) return Results.NotFound();
      pizza.Name = updatepizza.Name ?? pizza.Name;
      pizza.Description = updatepizza.Description ?? pizza.Description;
      pizza.Price = updatepizza.Price != 0 ? updatepizza.Price : pizza.Price;
      pizza.IsAvailable = updatepizza.IsAvailable;
      await db.SaveChangesAsync();
      return Results.NoContent();
});
app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) =>
{
        var pizza = await db.Pizzas.FindAsync(id);
        if (pizza is null) return Results.NotFound();
        db.Pizzas.Remove(pizza);
        await db.SaveChangesAsync();
        return Results.NoContent();
});

app.Run();
