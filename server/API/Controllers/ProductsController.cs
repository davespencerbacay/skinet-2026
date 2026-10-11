using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastracture.Data;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await repository.GetProductsAsync());
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await repository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.AddProduct(product);

        if (await repository.SaveChangesAsync())
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

        repository.UpdateProduct(product);

        if (await repository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("[Products] Problem updating product.");

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repository.GetProductByIdAsync(id);

        if (product == null) return NotFound();
        repository.DeleteProduct(product);

        if (await repository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("[Products] Problem deleting product.");
    }

    private bool ProductExists(int id)
    {
        return repository.ProductExists(id);
    }


}