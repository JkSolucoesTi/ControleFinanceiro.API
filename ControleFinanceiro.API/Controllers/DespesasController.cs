using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesaRepositorio _despesaRepositorio;

        public DespesasController(IDespesaRepositorio despesaRepositorio)
        {
            _despesaRepositorio = despesaRepositorio;
        }
        // GET: api/<DespesasController>
        [HttpGet("PegarDespesasPeloUsuarioId/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Despesa>>> PegarDespesasPeloUsuarioId(string usuarioId)
        {
            return await _despesaRepositorio.PegarDespesaPeloUsuarioId(usuarioId).ToListAsync();
        }
        
        // GET api/<DespesasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Despesa>>  GetDespesa(int id)
        {
            Despesa despesa = await _despesaRepositorio.PegarPeloId(id);

            if(despesa == null)
            {
                return NotFound();
            }

            return despesa;
        }

        // POST api/<DespesasController>
        [HttpPost]
        public async Task<ActionResult<Despesa>> PostDespesa(Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                await _despesaRepositorio.Inserir(despesa);
                return Ok(new
                {
                    mensagem = $"Despesa no valor de {despesa.Valor} criada com sucesso"
                }); ;
            }
            else
            {
                return BadRequest(despesa);
            }
        }

        // PUT api/<DespesasController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Despesa>> PutDespesa(int id, Despesa despesa)
        {
            if(id != despesa.DespesaId)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                await _despesaRepositorio.Atualizar(despesa);
                return Ok(new
                {
                    mensagem = $"Despesa no valor de {despesa.Valor} atualizada com sucesso"
                });
            }

            return BadRequest(despesa);
        }

        // DELETE api/<DespesasController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDespesa(int id)
        {
            Despesa despesa = await _despesaRepositorio.PegarPeloId(id);

            if(despesa == null)
            {
                return BadRequest();
            }

            await _despesaRepositorio.Excluir(despesa);

            return Ok(new
            {
                mensagem = $"Despesa {despesa.Valor} excluida com sucesso"
            });
        }
    }
}
