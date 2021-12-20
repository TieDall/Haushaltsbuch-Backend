using BusinessModels;
using DataServices.Entities;
using DataServices.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Controller
{
    public abstract class CrudController<TModel, TEntity> : ControllerBase
        where TModel : BaseModel
        where TEntity : BaseEntity
    {
        public IDataService<TModel, TEntity> _dataService;

        public CrudController(
            IDataService<TModel, TEntity> dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public async Task<ActionResult<TModel>> Add(TModel model)
        {
            var result = await _dataService.Add(model);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<TModel>> Get()
        {
            return Ok(await _dataService.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> Get(long id)
        {
            return Ok(await _dataService.Get(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TModel>> Update(long id, TModel model)
        {
            return Ok(await _dataService.Update(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _dataService.Delete(id);
            return Ok();
        }
    }
}
