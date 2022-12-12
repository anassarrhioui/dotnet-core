using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductManagementSystem.Data;
using ProductManagementSystem.Model;

namespace ProductManagementSystem.Controllers;


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