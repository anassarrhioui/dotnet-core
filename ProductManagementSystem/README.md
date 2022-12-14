## Entity Category
```csharp
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
```

## Entity Product
```csharp 
public class Product
{

    public int ProductId { get; set; }
    public string Name { get; set; }
    public double price { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

}
```

## CatalogueDbRepository
```csharp
public class CatalogueDbRepository : DbContext
{
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public CatalogueDbRepository(DbContextOptions options) : base(options)
    {
    }
}
```

## CategoryController
```csharp
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{

    private readonly CatalogueDbRepository _categoryDbRepository;

    public CategoryController(CatalogueDbRepository categoryDbRepository)
    {
        _categoryDbRepository = categoryDbRepository;
    }

    [HttpGet]
    public ActionResult<List<Category>> GetAllCategories()
    {
        var categories =  _categoryDbRepository.Categories;
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryById(int id)
    {
        var savedCategory = _categoryDbRepository.Categories.FindAsync(id);

        if (savedCategory.Result == null)
            return NotFound("No Category Found with id " + id);
        
        return savedCategory.Result;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> SaveCategory([FromBody] Category category)
    {
        EntityEntry<Category> savedCategory = await _categoryDbRepository.Categories.AddAsync(category);
        await _categoryDbRepository.SaveChangesAsync();
        return Ok(savedCategory.Entity);
    }

    [HttpPut("{id}")]
    public ActionResult<Category> UpdateCategory([FromBody] Category category, int id)
    {
        var savedCategory = _categoryDbRepository.Categories.Find(id);

        if (savedCategory == null)
            return NotFound("No Category Found with id " + id);

        var updatedCategory = _categoryDbRepository.Categories.Update(category);
        _categoryDbRepository.SaveChanges();
        return updatedCategory.Entity;
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(int id)
    {
        var savedCategory = _categoryDbRepository.Categories.Find(id);

        if (savedCategory == null)
            return NotFound("No Category Found with id " + id);

        _categoryDbRepository.Categories.Remove(savedCategory);
        _categoryDbRepository.SaveChanges();

        return Ok();
    }
    
    
}
```

## ProductController
```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{

    private readonly CatalogueDbRepository _categoryDbRepository;

    public ProductController(CatalogueDbRepository categoryDbRepository)
    {
        _categoryDbRepository = categoryDbRepository;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAllProducts()
    {
        var products =  _categoryDbRepository.Products;
        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProductById(int id)
    {
        var savedProduct = _categoryDbRepository.Products.FindAsync(id);

        if (savedProduct.Result == null)
            return NotFound("No Product Found with id " + id);
        
        return savedProduct.Result;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> SaveProduct([FromBody] Product product)
    {
        EntityEntry<Product> savedProduct = await _categoryDbRepository.Products.AddAsync(product);
        await _categoryDbRepository.SaveChangesAsync();
        return Ok(savedProduct.Entity);
    }

    [HttpPut("{id}")]
    public ActionResult<Product> UpdateProduct([FromBody] Product product, int id)
    {
        var savedProduct = _categoryDbRepository.Products.Find(id);

        if (savedProduct == null)
            return NotFound("No Product Found with id " + id);

        var updatedProduct = _categoryDbRepository.Products.Update(product);
        _categoryDbRepository.SaveChanges();
        return updatedProduct.Entity;
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var savedProduct = _categoryDbRepository.Products.Find(id);

        if (savedProduct == null)
            return NotFound("No Product Found with id " + id);

        _categoryDbRepository.Products.Remove(savedProduct);
        _categoryDbRepository.SaveChanges();

        return Ok();
    }
    
    
}
```

## Program
```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogueDbRepository>(options => options.UseInMemoryDatabase("DB_CAT"));

var app = builder.Build();
```