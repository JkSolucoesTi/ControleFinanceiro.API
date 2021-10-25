using ControleFinanceiro.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Mapeamento
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.CPF).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.CPF).IsUnique();

            builder.Property(u => u.Profissao).IsRequired().HasMaxLength(30);

            builder.Property(u => u.Email).HasMaxLength(50);

            builder.Property(u => u.Foto).IsRequired();

            builder.HasMany(u => u.Cartoes)
                .WithOne(c => c.Usuario).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Ganhos)
                .WithOne(c => c.Usuario).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Despesas)
                .WithOne(d => d.Usuario).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Usuarios");
        }
    }
}
