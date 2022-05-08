using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTeste.Domain.Repositories
{
   public interface ITesteRepository
   {
      Task<IList<Teste>> GetAllAsync();
      Task<Teste> GetIdAsync(int id);
      Task<Teste> AddAsync(Teste teste);
      Task<Teste> UpdateAsync(Teste teste);
      Task DeleteAsync(Teste teste);

   }
}
