using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Models;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleventumsController : ControllerBase
    {
        private readonly Pruebav4Context _context;

        public DetalleventumsController(Pruebav4Context context)
        {
            _context = context;
        }

        // GET: api/Detalleventums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalleventum>>> GetDetalleventa()
        {
            return await _context.Detalleventa.ToListAsync();
        }

        // GET: api/Detalleventums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalleventum>> GetDetalleventum(int id)
        {
            var detalleventum = await _context.Detalleventa.FindAsync(id);

            if (detalleventum == null)
            {
                return NotFound();
            }

            return detalleventum;
        }

        // PUT: api/Detalleventums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleventum(int id, Detalleventum detalleventum)
        {
            if (id != detalleventum.IdDetalle)
            {
                return BadRequest();
            }

            _context.Entry(detalleventum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleventumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Detalleventums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalleventum>> PostDetalleventum(Detalleventum detalleventum)
        {
            _context.Detalleventa.Add(detalleventum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleventum", new { id = detalleventum.IdDetalle }, detalleventum);
        }

        [HttpPost("Lista")]
        public async Task<IActionResult> PostListaDetalleventa(List<Detalleventum> detalles)
        {
            if (detalles == null || !detalles.Any())
            {
                return BadRequest("No se enviaron detalles.");
            }

            _context.Detalleventa.AddRange(detalles);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Detalles guardados correctamente." });
        }


        // DELETE: api/Detalleventums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleventum(int id)
        {
            var detalleventum = await _context.Detalleventa.FindAsync(id);
            if (detalleventum == null)
            {
                return NotFound();
            }

            _context.Detalleventa.Remove(detalleventum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleventumExists(int id)
        {
            return _context.Detalleventa.Any(e => e.IdDetalle == id);
        }
    }
}
