using FN.Store.Domain.Contracts.Repositories;
using FN.StoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FN.StoreApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoriasController:ControllerBase
    {
        private readonly ICateogriaRepository _categoriaRepository;
        private readonly IMemoryCache _memoryCache;

        public CategoriasController(ICateogriaRepository categoriaRepository, IMemoryCache memoryCache)
        {
            _categoriaRepository = categoriaRepository;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = (await _categoriaRepository.GetAsync())
                .Select(c=>c.TotCategoriaGet());
            return Ok(data);
        }

        [HttpGet("{id}",Name ="GetCategoriaById")]
        public async Task<IActionResult> GetById(int id)
        {

            if (!_memoryCache.TryGetValue(id, out var responseData))
            {
                responseData = await _categoriaRepository.GetAsync(id);

                //if (responseData == null) return NotFound();

                //return Ok(data.TotCategoriaGet());

                //O tempo que os dados ficam na cache
                var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    //AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(50),
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };

                _memoryCache.Set(id, responseData, memoryCacheEntryOptions);
            }

       
            return Ok(responseData);

        }

        [HttpPost]
        public  IActionResult Add([FromBody] CategoriaAddEdit model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var data = model.ToCategoria();

            _categoriaRepository.Add(data);

            return CreatedAtRoute("GetCategoriaById", new { data.Id }, data.TotCategoriaGet());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoriaAddEdit model)
        {
            var data = await _categoriaRepository.GetAsync(id);

            if (data == null)
                ModelState.AddModelError("Id", "Categoria não localizada");
            
            if (!ModelState.IsValid) return BadRequest(ModelState);

            data.Update(model.Nome, model.Descricao);
            
            _categoriaRepository.Update(data);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _categoriaRepository.GetAsync(id);

            if (data == null)
                return BadRequest(new { Categoria = new string[] { "Categoria não localizado" } });

            _categoriaRepository.Delete(data);

            return Ok();

        }


    }
}
