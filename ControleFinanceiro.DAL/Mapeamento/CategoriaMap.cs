using ControleFinanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Mapeamento
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(t => t.CategoriaId);
            builder.Property(t => t.Nome).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Icone).IsRequired().HasMaxLength(15);

            builder.HasOne(t => t.Tipo)
                .WithMany(t => t.Categorias)
                .HasForeignKey(t => t.TipoId)
                .IsRequired();

            builder.HasMany(t => t.Despesas).WithOne(t => t.Categoria);
            builder.HasMany(t => t.Ganhos).WithOne(t => t.Categoria);

            builder.ToTable("Categorias");

        }
    }
}
