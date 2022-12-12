using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly DataContext _dataContext;

    public ProductsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }


    [HttpGet()]
    public IEnumerable<Product> GetProducts()
    {
        return _dataContext.Products;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound("Product with Id " + id + " Not found");

        return product;
    }

    [HttpPost()]
    public async Task<ActionResult<Product>> SaveProduct([FromBody] Product product)
    {
        EntityEntry<Product> savedProduct = _dataContext.Products.Add(product);
        await _dataContext.SaveChangesAsync();
        return Ok(savedProduct.Entity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product, int id)
    {
        var savedProduct = await _dataContext.Products.FindAsync(id);
        if (savedProduct == null)
            return NotFound("Product with Id " + id + " Not found");

        product.Id = id;
        var updatedProduct = _dataContext.Products.Update(product);
        await _dataContext.SaveChangesAsync();
        return Ok(updatedProduct.Entity);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var savedProduct = await _dataContext.Products.FindAsync(id);
        if (savedProduct == null)
            return NotFound("Product with Id " + id + " Not found");
        _dataContext.Products.Remove(savedProduct);
        await _dataContext.SaveChangesAsync();
        return Ok();
    }
}