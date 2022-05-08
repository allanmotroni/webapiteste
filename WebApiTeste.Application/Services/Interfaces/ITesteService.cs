using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTeste.Domain;

namespace WebApiTeste.Application.Services.Interfaces
{
   public interface ITesteService
   {
      Task<IList<Teste>> GetAllAsync();
      Task<Teste> GetIdAsync(int id);
      
      Task<Teste> AddAsync(Teste teste);
      Task<Teste> UpdateAsync(int id, Teste teste);
      Task DeleteAsync(int id);
   }
}
