using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Model;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required, MinLength(2)]
    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; }

    public Category(int categoryId, string name)
    {
        CategoryId = categoryId;
        Name = name;
    }
}