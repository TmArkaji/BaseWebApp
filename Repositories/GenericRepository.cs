using BaseWebApplication.Data;
using BaseWebApplication.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Linq;
using System.Security.Claims;
using BaseWebApplication.Configurations;

namespace BaseWebApplication.Repositories
{
    public class GenericRepository<TKey, TModel> : IGenericRepository<TKey, TModel>
        where TModel : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly string _userId;

        public GenericRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _userId = (_httpContextAccessor.HttpContext == null) ? "null" : (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ? _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value : Constants.ANONYMOUSE_USER_ID);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public virtual Task<List<TModel>> GetAllAsync()
        {
            return _context.Set<TModel>().Where(e => !e.Eliminado).ToListAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(TKey id)
        {
            return await _context.Set<TModel>().FindAsync(id);
        }

        public virtual async Task<TModel> CreateAsync(TModel entity)
        {
            entity = AddCreateData(entity);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TModel> UpdateAsync(TModel entity)
        {
            entity = AddUpdateData(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public virtual TModel AddCreateData(TModel entity)
        {
            entity.CreateDate = GetDateTime();
            entity.UpdateDate = GetDateTime();
            entity.CreateUserId = _userId;
            entity.UpdateUserId = _userId;
            return entity;
        }

        public virtual TModel AddUpdateData(TModel entity)
        {
            entity.UpdateDate = GetDateTime();
            entity.UpdateUserId = _userId;

            var updateEntity = _context.Set<TModel>().Find(entity.ID);
            if (updateEntity != null)
            {
                entity.CreateDate = updateEntity.CreateDate;
                entity.CreateUserId = updateEntity.CreateUserId;
                _context.Entry(updateEntity).State = EntityState.Detached;
            }
            return entity;
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await _context.Set<TModel>().FindAsync(id);
            if (entity != null)
            {
                entity = AddUpdateData(entity);
                entity.Eliminado = true;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }

        protected async Task<string> GenerateUniqueIdAsync()
        {
            string newId;
            bool exists;

            do
            {
                newId = Guid.NewGuid().ToString();
                exists = await _context.Set<TModel>().AnyAsync(e => EF.Property<string>(e, "ID") == newId);
            } while (exists);

            return newId;
        }

        public virtual async Task<bool> Exist(TKey id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public virtual async Task<IEnumerable<SelectListItem>> GetDropDownList(Expression<Func<TModel, string>> textProperty, Expression<Func<TModel, TKey>> valueProperty, Expression<Func<TModel, bool>>? predicate = null)
        {
            IQueryable<TModel> query = _context.Set<TModel>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var items = await query.Select(entity => new SelectListItem
            {
                Value = valueProperty.Compile()(entity).ToString(),
                Text = textProperty.Compile()(entity)
            }).ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return items;
        }
    }
}
