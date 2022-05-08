using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTeste.Domain;

namespace WebApiTeste.Infrastructure.Configuration
{
   public class TesteConfiguration : IEntityTypeConfiguration<Teste>
   {
      public void Configure(EntityTypeBuilder<Teste> builder)
      {
         builder.HasKey(p => p.Id);

         builder.Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired();

         builder.Property(p => p.Descricao)
            .HasColumnName("Descricao")
            .HasColumnType("varchar(100)")
            .IsRequired();

         builder.Property(p => p.DataCriacao)
            .HasColumnName("DataCriacao")
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now)
            .IsRequired();

         builder.Property(p => p.DataAlteracao)
            .HasColumnName("DataAlteracao")
            .HasColumnType("datetime");

         builder.ToTable("Teste");
      }
   }
}
