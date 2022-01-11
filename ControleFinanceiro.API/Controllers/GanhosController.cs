using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GanhosController : ControllerBase
    {
        private readonly IGanhosRepositorio _ganhosRepositorio;
        public GanhosController(IGanhosRepositorio ganhosRepositorio)
        {
            _ganhosRepositorio = ganhosRepositorio;
        }


        [HttpGet("PegarGanhosPeloUsuarioId/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Ganho>>> PegarGanhosPeloUsuarioId(string usuarioId)
        {
            return await _ganhosRepositorio.PegarGanhoPeloUsuarioId(usuarioId).ToListAsync();
        }

        [HttpGet("{ganhoId}")]
        public async Task<ActionResult<Ganho>> GetGanho(int ganhoId)
        {
            Ganho ganho = await _ganhosRepositorio.PegarPeloId(ganhoId);

            if(ganho == null)
            {
                return NotFound();
            }

            return ganho;
        }

        [HttpPut("{ganhoId}")]
        public async Task<ActionResult<Ganho>> PutGanho(int ganhoId, Ganho ganho)
        {
            if(ganhoId != ganho.GanhoId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _ganhosRepositorio.Atualizar(ganho);

                return Ok(new
                {
                    mensagem = $"Ganho {ganho.Valor} atualizado com sucesso"
                });
            }
            else
            {
                return BadRequest(ganho);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Ganho>> PostGanho(Ganho ganho)
        {
            if (ModelState.IsValid)
            {
                await _ganhosRepositorio.Inserir(ganho);

                return Ok(new
                {
                    mensagem = $"Ganho {ganho.Valor} inserido com sucesso"
                });

            }
            else
            {
                return BadRequest(ganho);
            }
        }

        [HttpDelete("{ganhoId}")]
        public async Task<ActionResult> DeleteGanho(int ganhoId)
        {
            Ganho ganho = await _ganhosRepositorio.PegarPeloId(ganhoId);

            if(ganho == null)
            {
                return NotFound();
            }

            await _ganhosRepositorio.Excluir(ganho);
            
            return Ok(new
            {
                mensagem = $"Ganho {ganho.Valor} excluído com sucesso"
            });

        }

        [HttpGet("FiltrarGanhos/{nomeCategoria}")]
        public async Task<IEnumerable<Ganho>> FiltrarGanhos(string nomeCategoria)
        {
            return await _ganhosRepositorio.FiltrarGanhos(nomeCategoria).ToListAsync();
        }
    }
}
