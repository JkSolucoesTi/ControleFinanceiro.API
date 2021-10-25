using ControleFinanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Mapeamento
{
    public class CadastroMap : IEntityTypeConfiguration<Cadastro>
    {
        public void Configure(EntityTypeBuilder<Cadastro> builder)
        {
            builder.HasKey(c => c.CadastroId);

            builder.Property(c => c.Nome).HasMaxLength(255);
            builder.Property(c => c.Nome).IsRequired();
            builder.Property(c => c.Endereco).HasMaxLength(255);
            builder.Property(c => c.Endereco).IsRequired();
            builder.Property(c => c.Numero).HasMaxLength(20);
            builder.Property(c => c.Numero).IsRequired();
            builder.Property(c => c.CEP).HasMaxLength(10);
            builder.Property(c => c.CEP).IsRequired();
            builder.Property(c => c.DataNascimento).IsRequired();


            builder.HasOne(s => s.Sexo).WithMany(c => c.Cadastros)
                .HasForeignKey(s => s.SexoId);
            
            builder.ToTable("Cadastro");

        }
    }
}
