using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : Controller
    {
        private readonly ICadastroRepositorio _cadastroRepositorio;

        public CadastroController(ICadastroRepositorio cadastroRepositorio)
        {
            _cadastroRepositorio = cadastroRepositorio;
        }

        [HttpPost]
        public async Task<ActionResult<Cadastro>> NovoCadastro(Cadastro cadastro)
        {
            await _cadastroRepositorio.Inserir(cadastro);

            return Ok(new
            {
                mensagem = $"Cadastro {cadastro.Nome} inserido com sucesso"
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cadastro>>> PegarTodos()
        {
            return await _cadastroRepositorio.PegarTodos().ToListAsync();
        }

        [HttpGet("PegarPorId/{cadastroId}")]
        public async Task<ActionResult<Cadastro>> PegarPorId(int cadastroId)
        {
            var resultado = await _cadastroRepositorio.PegarPeloId(cadastroId);
            return resultado;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cadastro>> PutCadastro(int id,Cadastro cadastro)
        {
            if(id == cadastro.CadastroId)
            {
                await _cadastroRepositorio.Atualizar(cadastro);
                return Ok(
                    new
                    {
                        mensagem = "cadastro atualizado com sucesso !!"
                    }); ;
            }

            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cadastro>> DeleteCadastro(int id)
        {
            if(id != 0)
            {
                var cadastro = await _cadastroRepositorio.PegarPeloId(id);
                await _cadastroRepositorio.Excluir(cadastro);
                return Ok(
                    new
                    {
                        mensagem = "Cadastro excluído com sucesso"
                    }
                );; 
            }

            return NotFound();
            
        }
    }
}
