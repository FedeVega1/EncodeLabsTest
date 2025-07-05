using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncodeLabsTest.Models;

namespace EncodeLabsTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        readonly ProductosContext _context;

        public ProductosController(ProductosContext context) => _context = context;

        [HttpPost("add")]
        public async Task<ActionResult<Product>> PostProducto([FromBody] ProductDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Product product = ConvertDTOToProduct(dto);

            try
            {
                _context.Productos.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest($"An exception was thrown while saving in the database: {e.Message}");
            }

            return CreatedAtAction(nameof(GetProducto), new { id = product.Id }, product);
        }

        [HttpGet("enlist")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductos()
        {
            List<Product> products = await _context.Productos.ToListAsync();
            List<ProductDTO> dtos = products.Select(ConvertProductToDTO).ToList();

            return dtos;
        }

        [HttpGet("find/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProducto(long id)
        {
            Product? producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound("Requested ID was not found");

            return ConvertProductToDTO(producto);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutProducto(long id, [FromBody] ProductDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Product? product = await _context.Productos.FindAsync(id);
            if (product == null) return NotFound($"Requested product with id {id} couldn't be found");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.Id == id)) return NotFound();
                else return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteProducto(long id)
        {
            Product? producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound("Requested ID was not found");

            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest($"An Exception was thrown while removing an element from the database: {e.Message}");
            }

            return NoContent();
        }

        static Product ConvertDTOToProduct(ProductDTO dto) => new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Quantity = dto.Quantity
        };

        static ProductDTO ConvertProductToDTO(Product product) => new ProductDTO
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity
        };
    }
}
