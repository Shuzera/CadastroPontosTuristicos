using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCadastroLocais_API.Context;
using SistemaCadastroLocais_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadastroLocais_API.Controllers
{

    //Caminho da URL
    [Route("api/[controller]")]
    [ApiController]
    
    public class PontoTuristicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        //Instanciando construtor e referenciando o context
        public PontoTuristicoController(AppDbContext context)
        {
            _context = context;
        } // Fim do construtor

        // GET: api/Locais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_PontosTuristicos>>> GetLocais(string search)
        {

            if (string.IsNullOrEmpty(search))
                search = "";

            var list = _context.locais.Where(x => x.Nome.Contains(search));

            return Ok(list);
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<TB_PontosTuristicos>>GetTB_Locais(int id)
        {

            var retorno = _context.locais.FirstOrDefault(x => x.LocaisId == id);

            return Ok(retorno);
        }  
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTB_Locais(int id, TB_PontosTuristicos tb_PontosTuristicos)
        {
            if (id != tb_PontosTuristicos.LocaisId)
            {
                return BadRequest();
            }

            _context.Entry(tb_PontosTuristicos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TB_LocaisExists(id))
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

        [HttpPost]
        public async Task<ActionResult<TB_PontosTuristicos>> PostTB_Locais(TB_PontosTuristicos tb_PontosTuristicos)
        {
            _context.locais.Add(tb_PontosTuristicos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTB_locais", new { id = tb_PontosTuristicos.LocaisId}, tb_PontosTuristicos);
        }

        // DELETE: api/Locais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTB_locais(int id)
        {
            var tB_Locais = await _context.locais.FindAsync(id);
            if (tB_Locais == null)
            {
                return NotFound();
            }

            _context.locais.Remove(tB_Locais);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TB_LocaisExists(int id)
        {
            return _context.locais.Any(e => e.LocaisId == id);
        }
    }
  
}
