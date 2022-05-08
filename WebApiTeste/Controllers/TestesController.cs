using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTeste.Application.Services.Interfaces;
using WebApiTeste.Domain;

namespace WebApiTeste.Controllers
{
   /// <summary>
   /// Testes
   /// </summary>
   [Route("api/Testes")]
   [ApiController]
   public class TestesController : ControllerBase
   {
      private readonly ITesteService _testeService;
      public TestesController(ITesteService testeService)
      {
         _testeService = testeService;
      }

      /// <summary>
      /// Get All
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      [Consumes("application/json")]
      [Produces("application/json")]
      [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IList<Teste>))]
      [SwaggerResponse(StatusCodes.Status404NotFound)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> GetAll()
      {
         try
         {
            var response = await _testeService.GetAllAsync();
            if (response.Count == 0)
            {
               return NotFound();
            }

            return Ok(response);
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }

      /// <summary>
      /// Get by Id
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id:int}")]
      [Consumes("application/json")]
      [Produces("application/json")]
      [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Teste))]
      [SwaggerResponse(StatusCodes.Status404NotFound)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Get([FromRoute]int id)
      {
         try
         {
            var response = await _testeService.GetIdAsync(id);
            if (response == null)
            {
               return NotFound();
            }

            return Ok(response);
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }

      /// <summary>
      /// Put
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpPost]
      [Consumes("application/json")]
      [Produces("application/json")]
      [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Teste))]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Add([FromBody]Teste request)
      {
         try
         {
            var response = await _testeService.AddAsync(request);
            
            return CreatedAtAction(null, response);
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }

      /// <summary>
      /// Update
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      [HttpPut("{id:int}")]
      [Consumes("application/json")]
      [Produces("application/json")]
      [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Teste))]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Update([FromRoute]int id, [FromBody] Teste request)
      {
         try
         {
            var response = await _testeService.UpdateAsync(id, request);

            return Ok(response);
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }

      [HttpDelete("{id:int}")]
      [Consumes("application/json")]
      [Produces("application/json")]
      [SwaggerResponse(StatusCodes.Status204NoContent)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Delete([FromRoute] int id)
      {
         try
         {
            await _testeService.DeleteAsync(id);

            return NoContent();
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }
   }
}
