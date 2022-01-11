using ControleFinanceiro.API.Services;
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
        public async Task<ActionResult<AtualizarUsuarioViewModel>> PegarPeloId(string id)
        {
            var usuario = await _usuarioRepositorio.PegarPeloId(id);

            if(usuario == null)
            {
                return NotFound();
            }

            AtualizarUsuarioViewModel model = new AtualizarUsuarioViewModel
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Profissao = usuario.Profissao,
                Foto = usuario.Foto

            };

            return model;
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
                    Email = model.Email,
                    PasswordHash = model.Senha,
                    CPF = model.CPF,
                    Profissao = model.Profissao,
                    Foto = model.Foto
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
                    var token = TokenService.GerarToken(usuario, funcaoUsuario);
                    await _usuarioRepositorio.LogarUsuario(usuario, false);

                    return Ok(new
                    {
                        emailUsuarioLogado = model.Email,
                        usuarioId = usuario.Id,
                        tokenUsuarioLogado = token
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

        [HttpPost("LogarUsuario")]
        public async Task<ActionResult> LogarUsuario(LoginViewModel model)
        {
            if(model == null)
            {
                return NotFound("Usuario ou Senha inválidos");
            }

            Usuario usuario = await _usuarioRepositorio.PegarPeloEmail(model.Email);

            if(usuario != null)
            {
                PasswordHasher<Usuario> password = new PasswordHasher<Usuario>();
                if(password.VerifyHashedPassword(usuario,usuario.PasswordHash,model.Senha) != PasswordVerificationResult.Failed)
                {
                    var funcaoUsuario = await _usuarioRepositorio.PegarFuncoesUsuario(usuario);
                    var token = TokenService.GerarToken(usuario, funcaoUsuario.First());
                    await _usuarioRepositorio.LogarUsuario(usuario, false);
                    return Ok(new
                    {
                        emailUsuarioLogado = usuario.Email,
                        usuarioId = usuario.Id,
                        tokenUsuarioLogado = token
                    });
                }
            }          
            
            return NotFound("Usuario e ou Senha inválidos");
            
        }   
        

        [HttpGet("RetornarFotoUsuario/{usuarioId}")]
        public async Task<dynamic> RetornarFotoUsuario(string usuarioId)
        {
            Usuario usuario = await _usuarioRepositorio.PegarPeloId(usuarioId);

            return new { imagem = usuario.Foto };
        }

        [HttpPut("AtualizarUsuario")]
        public async Task<ActionResult> AtualizarUsuario(AtualizarUsuarioViewModel atualizarUsuario)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.PegarPeloId(atualizarUsuario.Id);
                usuario.UserName = atualizarUsuario.UserName;
                usuario.Email = atualizarUsuario.Email;
                usuario.CPF = atualizarUsuario.CPF;
                usuario.Profissao = atualizarUsuario.Profissao;
                usuario.Foto = atualizarUsuario.Foto;

                await _usuarioRepositorio.AtualizarUsuario(usuario);

                return Ok(new
                {
                    mensagem = $"Usuario {usuario.Email} atualizado com sucesso"
                });


            }

            return BadRequest(atualizarUsuario);
        }


    }
}