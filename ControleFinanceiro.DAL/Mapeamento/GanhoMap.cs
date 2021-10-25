using ControleFinanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Mapeamento
{
    public class GanhoMap : IEntityTypeConfiguration<Ganho>
    {
        public void Configure(EntityTypeBuilder<Ganho> builder)
        {
            builder.HasKey(g => g.GanhoId);
            builder.Property(g => g.Descricao).IsRequired().HasMaxLength(50);
            builder.Property(g => g.Valor).IsRequired();
            builder.Property(g => g.Dia).IsRequired();
            builder.Property(g => g.Ano).IsRequired();

            /*ganho tem um categoria que pertence a varios ganhos*/

            builder.HasOne(g => g.Categoria)
                .WithMany(c => c.Ganhos)
                .HasForeignKey(c => c.CategoriaId)
                .IsRequired();

            builder.HasOne(g => g.Mes)
                .WithMany(c => c.Ganhos)
                .HasForeignKey(m => m.MesId)
                .IsRequired();

            builder.HasOne(g => g.Usuario)
                .WithMany(g => g.Ganhos)
                .HasForeignKey(u => u.UsuarioId)
                .IsRequired();

            builder.ToTable("Ganhos");

        }
    }
}
