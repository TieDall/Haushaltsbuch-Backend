using BusinessModels;
using DataServices.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServices.Services.Base
{
    public interface IDataService<TModel, TEntity> 
        where TModel : BaseModel 
        where TEntity : BaseEntity
    {
        Task<TModel> Get(long id);

        Task<IEnumerable<TModel>> Get();

        Task<TModel> Add(TModel model);

        Task<TModel> Update(TModel model);

        Task Delete(long id);

        IQueryable<TEntity> GetDefaultQuery();
    }
}
