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
    public class CartoesController : ControllerBase
    {
        private readonly ICartaoRepositorio _cartaoRepositorio;

        private readonly IDespesaRepositorio _despesaRepositorio;


        public CartoesController(ICartaoRepositorio cartaoRepositorio , IDespesaRepositorio despesaRepositorio)
        {
            _cartaoRepositorio = cartaoRepositorio;
            _despesaRepositorio = despesaRepositorio;
        }

        [HttpGet("PegarCartoesPeloUsuarioId/{usuarioId}")]
        public async Task<IEnumerable<Cartao>> PegarCartoesPeloUsuarioId(string usuarioId)
        {
            try
            {
                return await _cartaoRepositorio.PegarCartaoPeloUsuarioId(usuarioId).ToListAsync() ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cartao>> GetCartao(int id)
        {
            try
            {
               var cartao = await _cartaoRepositorio.PegarPeloId(id);

                if(cartao == null)
                {
                    return NotFound();
                }
                else
                {
                    return cartao;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cartao>> PutCartao (int id,Cartao cartao)
        {
            try
            {
                if(id != cartao.CartaoId)
                {
                    return BadRequest("Cartões diferentes , não foi possível atualizar");
                }
                if (ModelState.IsValid)
                {
                    await _cartaoRepositorio.Atualizar(cartao);
                    return Ok(new
                    {
                        mensagem = "Cartão atualizado com sucesso"
                    });
                }

                return BadRequest(cartao);
              
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cartao>> PostCartao (Cartao cartao)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _cartaoRepositorio.Inserir(cartao);

                    return Ok(new
                    {
                        mensagem = "Cartao Inserido com sucesso"
                    });
                }

                return BadRequest(cartao);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCartao (int id)
        {
            try
            {
                var cartao = await _cartaoRepositorio.PegarPeloId(id);

                if(cartao == null)
                {
                    return NotFound("Cartao não encontrado");
                }

                IEnumerable<Despesa> despesas = await _despesaRepositorio.PegarDespesaPeloCartaoId(cartao.CartaoId);
                _despesaRepositorio.ExcluirDespesas(despesas);


                await _cartaoRepositorio.Excluir(cartao);

                return Ok(new
                {
                    mensagem = "Cartão excluído com sucesso"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("FiltrarCartoes/{numeroCartao}")]
        public async Task<IEnumerable<Cartao>> FiltrarCartoes(string numeroCartao)
        {
            try
            {
               return await _cartaoRepositorio.FiltrarCartoes(numeroCartao).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
