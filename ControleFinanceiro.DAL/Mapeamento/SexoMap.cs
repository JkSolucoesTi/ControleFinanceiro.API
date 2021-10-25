using ControleFinanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Mapeamento
{
    public class SexoMap : IEntityTypeConfiguration<Sexo>
    {
        public void Configure(EntityTypeBuilder<Sexo> builder)
        {
            builder.HasKey(s => s.SexoId);
            builder.Property(s => s.Tipo).HasMaxLength(50);
            builder.Property(s => s.Tipo).IsRequired();

            builder.HasMany(c => c.Cadastros).WithOne(s => s.Sexo);

            builder.ToTable("Sexo");
        }
    }
}
