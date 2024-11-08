using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using BaseWebApplication.Data;

namespace BaseWebApplication.Interfaces
{
    public interface IGenericRepository<TKey, TModel> where TModel : BaseEntity<TKey>
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(TKey id);
        Task<TModel> CreateAsync(TModel entity);
        Task<TModel> UpdateAsync(TModel entity);
        Task DeleteAsync(TKey id);
        Task<bool> Exist(TKey id);
        public DateTime GetDateTime();
        Task<List<string>> ValidateModelAsync(TModel model, bool isUpdate);

        Task<IEnumerable<SelectListItem>> GetDropDownList(
            Expression<Func<TModel, string>> textProperty,
            Expression<Func<TModel, TKey>> valueProperty,
            Expression<Func<TModel, bool>>? predicate = null);

    }
}
