using AutoMapper;
using BusinessModels;
using DataServices.DbContexte;
using DataServices.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServices.Services.Base
{
    public class DataService<TModel, TEntity> : IDataService<TModel, TEntity>
        where TModel : BaseModel
        where TEntity : BaseEntity
    {
        private readonly HaushaltsbuchContext _haushaltsbuchContext;
        private readonly IMapper _mapper;

        public DataService(
            HaushaltsbuchContext haushaltsbuchContext,
            IMapper mapper)
        {
            _haushaltsbuchContext = haushaltsbuchContext;
            _mapper = mapper;
        }

        public async Task<TModel> Add(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            await _haushaltsbuchContext.AddAsync(entity);
            await _haushaltsbuchContext.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public async Task Delete(long id)
        {
            var entity = await _haushaltsbuchContext.FindAsync<TEntity>(id);

            _haushaltsbuchContext.Remove(entity);
            await _haushaltsbuchContext.SaveChangesAsync();
        }

        public async Task<TModel> Get(long id)
        {
            var entity = await GetDefaultQuery().FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<TModel>(entity);
        }

        public async Task<IEnumerable<TModel>> Get()
        {
            var entity = await GetDefaultQuery().ToListAsync();

            return _mapper.Map<List<TModel>>(entity);
        }

        public IQueryable<TEntity> GetDefaultQuery()
        {
            return _haushaltsbuchContext.Set<TEntity>();
        }

        public async Task<TModel> Update(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            _haushaltsbuchContext.Update(entity);
            await _haushaltsbuchContext.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }
    }
}
