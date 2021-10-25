using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
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
    public class FuncoesController : ControllerBase
    {
        private readonly IFuncaoRepositorio _funcaoRepositorio;
        public FuncoesController(IFuncaoRepositorio funcaoRepositorio)
        {
            _funcaoRepositorio = funcaoRepositorio;
        }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Funcao>>>GetFunao()
    {
        var funcao = await _funcaoRepositorio.PegarTodos().ToListAsync();
        return funcao;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Funcao>>GetFuncao(string id)
    {
        var funcao = await _funcaoRepositorio.PegarPeloId(id);

        if(funcao == null)
        {
                return NotFound();
        }
              
        return funcao;
    }

   [HttpPut("{id}")]
    public async Task<ActionResult<Funcao>> PutFuncao(string id, FuncoesViewModel funcoes)
    {

        if (id == funcoes.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            Funcao funcao = new Funcao
            {
                Id = funcoes.Id,
                Name = funcoes.Nome,
                Descricao = funcoes.Descricao
            };

            await _funcaoRepositorio.AtualizarFuncao(funcao);
        }

       return Ok( new
       {
            mensagem = "Funcao atualizado com sucesso"
       });
    }

    [HttpPost]
    public async Task<ActionResult<Funcao>>PostFuncao(FuncoesViewModel funcoes)
        {
            if(ModelState.IsValid)
            {
                Funcao f = new Funcao
                {
                    Name = funcoes.Nome,
                    Descricao = funcoes.Descricao
                };

                await _funcaoRepositorio.AdicionarFuncao(f);

                return Ok(new
                {
                    mensagem = "Funcao adicionada com sucesso"
                });
            }

            return BadRequest();
        }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Funcao>>DeleteFuncao(string id)
    {
     var funcao = await _funcaoRepositorio.PegarPeloId(id);

     if(funcao == null)
     {
         return BadRequest();
     }

     await _funcaoRepositorio.Excluir(funcao);

     return Ok(new
     {
         mensagem = "Funcao excluída com sucesso"
     });

    }

    [HttpGet("FiltrarFuncoes/{nomeFuncoes}")]
    public async Task<ActionResult<IEnumerable<Funcao>>> FiltrarFuncoes(string nomeFuncoes)
        {
            var funcoes = await _funcaoRepositorio
                .FiltrarFuncoes(nomeFuncoes).ToListAsync();
            return funcoes;
        }

    }
}
