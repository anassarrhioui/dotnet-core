using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(30)]
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; } = 0;
    public int Quantity { get; set; } = 0;

    public Product(int id, string name, double price, int quantity)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}