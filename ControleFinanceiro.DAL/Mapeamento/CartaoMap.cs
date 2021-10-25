using ControleFinanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Mapeamento
{
    public class CartaoMap : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            builder.HasKey(t => t.CartaoId);

            builder.Property(t => t.Nome).IsRequired().HasMaxLength(20);
            builder.HasIndex(t => t.Nome).IsUnique();
            builder.Property(t => t.Bandeira).IsRequired().HasMaxLength(15);
            builder.Property(t => t.Numero).IsRequired().HasMaxLength(20);
            builder.HasIndex(t => t.Numero).IsUnique();
            builder.Property(t => t.Limite).IsRequired();

            builder.HasOne(c => c.Usuario).WithMany(c => c.Cartoes)
            .HasForeignKey(c => c.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.Despesas).WithOne(t => t.Cartao);

            builder.ToTable("Cartoes");

        }
    }
}
