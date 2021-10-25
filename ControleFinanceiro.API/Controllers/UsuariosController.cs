using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        // GET: api/<UsuariosController>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> PegarPeloId(string id)
        {
            var usuario = await _usuarioRepositorio.PegarPeloId(id);

            if(usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost("SalvarFoto")]
        public async Task<ActionResult> SalvarFoto()
        {
            var foto = Request.Form.Files[0];/*Pegando o arquivo do request*/
            byte[] b;/*declarando uma variavel do tipo byte para armazenar a foto*/

            using(var openReadStream = foto.OpenReadStream())/*Realizando a abertura da foto*/
            {
                using(var memoryStream = new MemoryStream())/*instanciando um objeto para gravar em memoria*/
                {
                    await openReadStream.CopyToAsync(memoryStream);/*Passando para a memoria*/
                    b = memoryStream.ToArray();/*jogando o array de bits lido para a variavel b*/
                }
            }

            return Ok(new
            {
                foto = b
            });
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarUsuario(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult usuarioCriado;
                string funcaoUsuario;

                Usuario usuario = new Usuario
                {
                    UserName = model.NomeUsuario,
                 //   Email = model.Email,
                    PasswordHash = model.Senha,
                    CPF = model.CPF,
                    Profissao = model.Profissao,
                //    Foto = model.Foto
                };

                if(await _usuarioRepositorio.PegarQuantidadeUsuariosRegistrados() > 0)
                {
                    funcaoUsuario = "Usuario";
                }
                else
                {
                    funcaoUsuario = "Administrador";
                }

                usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

                if (usuarioCriado.Succeeded)
                {
                    await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, funcaoUsuario);
                    await _usuarioRepositorio.LogarUsuario(usuario, false);

                    return Ok(new
                    {
                        emailUsuarioLogado = model.Email,
                        usuarioId = usuario.Id
                    });
                }
                else
                {
                    return BadRequest(model);
                }
            }

            return BadRequest(new { 
            mensagem =$"Criação fracassada {model}"}
            );


        }

       
    }
}
