using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repository) : BaseApiController
{

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);

        return await CreatePagedResult(
            repository,
            spec,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.Add(product);

        if (await repository.SaveAllAsync())
        {
            return CreatedAtAction("GetProductById", new { id = product.Id }, product);
        }

        return BadRequest("[Products] Problem creating product.");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
        {
            return BadRequest("[Products] Cannot update this product.");
        }

        repository.Update(product);

        if (await repository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("[Products] Problem updating product.");

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product == null) return NotFound();
        repository.Remove(product);

        if (await repository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("[Products] Problem deleting product.");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await repository.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await repository.ListAsync(spec));
    }

    private bool ProductExists(int id)
    {
        return repository.Exists(id);
    }


}