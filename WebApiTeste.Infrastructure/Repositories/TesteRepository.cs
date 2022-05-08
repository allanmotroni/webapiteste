using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTeste.Domain;
using WebApiTeste.Domain.Repositories;
using WebApiTeste.Infrastructure.Context;

namespace WebApiTeste.Infrastructure.Repositories
{
   public class TesteRepository : ITesteRepository
   {
      private readonly DatabaseContext _context;
      public TesteRepository(DatabaseContext context)
      {
         _context = context;
      }

      public async Task<Teste> GetIdAsync(int id)
      {
         return await _context.Teste.FindAsync(id);
      }

      public async Task<IList<Teste>> GetAllAsync()
      {
         var lista = await _context.Teste.ToListAsync();
         return lista;
      }

      public async Task<Teste> AddAsync(Teste teste)
      {
         await _context.Teste.AddAsync(teste);
         await _context.SaveChangesAsync();
         return teste;
      }

      public async Task<Teste> UpdateAsync(Teste teste)
      {
         _context.Teste.Update(teste);
         await _context.SaveChangesAsync();
         return teste;
      }

      public async Task DeleteAsync(Teste teste)
      {
         _context.Teste.Remove(teste);
         await _context.SaveChangesAsync();         
      }
   }
}
