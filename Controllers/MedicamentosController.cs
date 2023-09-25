using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIExemplo.Data;
using APIExemplo.Models;

namespace APIExemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly ExemploContext _context;

        public MedicamentosController(ExemploContext context)
        {
            _context = context;
        }

        // GET: api/Medicamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamentos>>> GetMedicamentos()
        {
          if (_context.Medicamentos == null)
          {
              return NotFound();
          }
            return await _context.Medicamentos.ToListAsync();
        }

        // GET: api/Medicamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamentos>> GetMedicamentos(int id)
        {
          if (_context.Medicamentos == null)
          {
              return NotFound();
          }
            var medicamentos = await _context.Medicamentos.FindAsync(id);

            if (medicamentos == null)
            {
                return NotFound();
            }

            return medicamentos;
        }

        // PUT: api/Medicamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamentos(int id, Medicamentos medicamentos)
        {
            if (id != medicamentos.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicamentos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentosExists(id))
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

        // POST: api/Medicamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicamentos>> PostMedicamentos(Medicamentos medicamentos)
        {
          if (_context.Medicamentos == null)
          {
              return Problem("Entity set 'ExemploContext.Medicamentos'  is null.");
          }

            if (medicamentos.AnoVencimento <= 2023)
            {
                return Problem("Ano de vencimento incorreto.");
            }
            else
            {
                _context.Medicamentos.Add(medicamentos);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetMedicamentos", new { id = medicamentos.Id }, medicamentos);
            }
            
        }

        // DELETE: api/Medicamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamentos(int id)
        {
            if (_context.Medicamentos == null)
            {
                return NotFound();
            }
            var medicamentos = await _context.Medicamentos.FindAsync(id);
            if (medicamentos == null)
            {
                return NotFound();
            }

            _context.Medicamentos.Remove(medicamentos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicamentosExists(int id)
        {
            return (_context.Medicamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
