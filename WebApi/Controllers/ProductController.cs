using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private DataContext _dataContext;

    public ProductController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    private static List<Product> _products = new List<Product>
    {
        new(id: 1, name: "Product 1", price: 1000, quantity: 12),
        new(id: 2, name: "Product 2", price: 1200, quantity: 20),
        new(id: 3, name: "Product 3", price: 440, quantity: 32)
    };

    [HttpGet("db")]
    public IEnumerable<Product> GetProducts()
    {
        return _dataContext.Products;
    }

    [HttpGet("db/{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = _dataContext.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound("Product with Id " + id + " Not found");
        
        return product;
    }

    [HttpPost("db")]
    public Product SaveProduct([FromBody] Product product)
    {
        EntityEntry<Product> savedProduct = _dataContext.Products.Add(product);
        _dataContext.SaveChanges();
        return savedProduct.Entity;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        return Ok(_products);
    }

    [HttpGet(template: "{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = _products.Find(p => p.Id == id);

        if (product == null)
            return NotFound("Product with Id " + id + " Not found");

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
    {
        product.Id = _products.Count + 1;
        _products.Add(product);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product, int id)
    {
        var indexOfProduct = _products.FindIndex(p => p.Id == id);

        if (indexOfProduct == -1)
            return NotFound("Product with Id " + id + " Not found");

        product.Id = id;
        _products[indexOfProduct] = product;

        return _products[indexOfProduct];
    }

    [HttpDelete(template: "{id}")]
    public async Task<ActionResult<Product>> DeleteProductById(int id)
    {
        var product = _products.Find(p => p.Id == id);

        if (product == null)
            return NotFound("Product with Id " + id + " Not found");
        _products.Remove(product);

        return Ok(_products);
    }
}