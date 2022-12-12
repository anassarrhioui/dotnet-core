using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductManagementSystem.Data;
using ProductManagementSystem.Model;

namespace ProductManagementSystem.Controllers;


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