using ControleFinanceiro.API.Extensions;
using ControleFinanceiro.API.Validacao;
using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.DAL.Repositorios;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("ConexaoBD")));

            services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<Contexto>();
            services.ConfigurarSenhaUsuario();
        
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<ITipoRepositorio, TipoRepositorio>();
            services.AddScoped<ICadastroRepositorio, CadastroRepositorio>();
            services.AddScoped<ISexoRepositorio, SexoRepositorio>();
            services.AddScoped<IFuncaoRepositorio, FuncaoRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            services.AddTransient<IValidator<Categoria>, CategoriaValidator>();
            services.AddTransient<IValidator<FuncoesViewModel>, FuncoesViewModelValidator>();
            services.AddTransient<IValidator<RegistroViewModel>, RegistroViewModelValidator>();


            services.AddCors();

            services.AddSpaStaticFiles(diretorio =>
            {
                diretorio.RootPath = "ControleFinanceiro-UI";
            });

            //Vamos adicionar a opção de ignorar os valores nulos
            services.AddControllers()
                .AddFluentValidation()
                .AddJsonOptions( opcoes =>
                {
                    opcoes.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddNewtonsoftJson(opcoes =>
                {
                    opcoes.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                ;                       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSpaStaticFiles();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(opcoes =>
           {
               opcoes.Options.SourcePath = ConfigurationPath.Combine(Directory.GetCurrentDirectory(), "ControleFinanceiro-UI");

               if (env.IsDevelopment())
               {
                   opcoes.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
               }

           });
        }
    }
}
