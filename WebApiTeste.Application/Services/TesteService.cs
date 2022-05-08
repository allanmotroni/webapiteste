using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTeste.Application.Services.Interfaces;
using WebApiTeste.Domain;
using WebApiTeste.Domain.Repositories;

namespace WebApiTeste.Application.Services
{
   public class TesteService : ITesteService
   {
      private readonly ITesteRepository _testeRepository;
      public TesteService(ITesteRepository testeRepository)
      {
         _testeRepository = testeRepository;
      }

      public async Task<Teste> GetIdAsync(int id)
      {
         return await _testeRepository.GetIdAsync(id);
      }

      public async Task<IList<Teste>> GetAllAsync()
      {
         return await _testeRepository.GetAllAsync();
      }

      public async Task<Teste> AddAsync(Teste teste)
      {
         //Validar

         teste.DataCriacao = DateTime.Now;
         return await _testeRepository.AddAsync(teste);
      }

      public async Task<Teste> UpdateAsync(int id, Teste teste)
      {
         //Validar

         var testeEncontrado = await _testeRepository.GetIdAsync(id);
         if (testeEncontrado == null)
            throw new KeyNotFoundException();

         testeEncontrado.DataAlteracao = DateTime.Now;
         testeEncontrado.Descricao = teste.Descricao;

         return await _testeRepository.UpdateAsync(testeEncontrado);
      }
      
      public async Task DeleteAsync(int id)
      {
         var testeEncontrado = await _testeRepository.GetIdAsync(id);
         if (testeEncontrado == null)
            throw new KeyNotFoundException();

         await _testeRepository.DeleteAsync(testeEncontrado);
      }
   }
}
