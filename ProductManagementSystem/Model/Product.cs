using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Model;

public class Product
{

    public int ProductId { get; set; }
    public string Name { get; set; }
    public double price { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

}