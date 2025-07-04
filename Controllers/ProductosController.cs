using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncodeLabsTest.Models;

namespace EncodeLabsTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosContext _context;

        public ProductosController(ProductosContext context) => _context = context;

        [HttpPost("add")]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        [HttpGet("enlist")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos() => await _context.Productos.ToListAsync();

        [HttpGet("find/{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            Producto? producto = await _context.Productos.FindAsync(id);

            if (producto == null) return NotFound();
            return producto;
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest();

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            Producto? producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id) => _context.Productos.Any(e => e.Id == id);
    }
}
