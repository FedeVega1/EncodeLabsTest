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

        /// <summary>
        /// POST endpoint - Adds a product into the database
        /// Body format:
        /// {
        ///     "name": "ProductName",
        ///     "description": "ProductDescription",
        ///     "price": ProductPrice,
        ///     "quantity": ProductQuantity
        /// }
        /// </summary>
        /// <param name="dto">The product object submitted by the user</param>
        /// <returns>201 if valid + the submitted product with its ID</returns>
        [HttpPost("add")]
        public async Task<ActionResult<Product>> PostProducto([FromBody] ProductDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_context.Productos.Any(x => x.Name == dto.Name)) return BadRequest("There is already a product with that name!");

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

        /// <summary>
        /// GET endpoint - Creates a list with all products that are in the database at the moment of the request
        /// </summary>
        /// <returns>A list of products inside the database</returns>
        [HttpGet("enlist")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductos() => await _context.Productos.ToListAsync();

        /// <summary>
        /// GET endpoint - Searches for a product by ID
        /// </summary>
        /// <param name="id">The id of the product to find</param>
        /// <returns>200 if found + the found product</returns>
        [HttpGet("find/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProducto(long id)
        {
            Product? product = await _context.Productos.FindAsync(id);
            if (product == null) return NotFound("Requested ID was not found");

            return ConvertProductToDTO(product);
        }

        /// <summary>
        /// PUT endpoint - Updates the specified product
        /// Body format:
        /// {
        ///     "name": "UpdatedName",
        ///     "description": "UpdatedDescription",
        ///     "price": UpdatedPrice,
        ///     "quantity": UpdatedQuantity
        /// }
        /// </summary>
        /// <param name="id">The id of the product to update</param>
        /// <param name="dto">The new product data</param>
        /// <returns>204 if found and no errors where found while updating</returns>
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

        /// <summary>
        /// DEL endpoint - Removes a product from the database
        /// </summary>
        /// <param name="id">The ID of the product to remove</param>
        /// <returns>204 if found</returns>
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

        /// <summary>
        /// Converts a DTO object into a Product object
        /// </summary>
        /// <param name="dto">The DTO object to convert</param>
        /// <returns>The converted Product</returns>
        static Product ConvertDTOToProduct(ProductDTO dto) => new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Quantity = dto.Quantity
        };

        /// <summary>
        /// Converts a Product object into a DTO object
        /// </summary>
        /// <param name="product">The Product to convert</param>
        /// <returns>The converted DTO object</returns>
        static ProductDTO ConvertProductToDTO(Product product) => new ProductDTO
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity
        };
    }
}
