using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTeste.Domain;
using WebApiTeste.Infrastructure.Configuration;

namespace WebApiTeste.Infrastructure.Context
{
   public class DatabaseContext : DbContext
   {
      public DatabaseContext(DbContextOptions options)
         : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Teste>(new TesteConfiguration().Configure);

         //base.OnModelCreating(modelBuilder);
      }

      public DbSet<Teste> Teste { get; set; }


   }
}
