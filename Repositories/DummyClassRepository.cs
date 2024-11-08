using BaseWebApplication.Data;
using BaseWebApplication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BaseWebApplication.Configurations.ExceptionsHandler;

namespace BaseWebApplication.Repositories
{
    public class DummyClassRepository : GenericRepository<string, DummyClass>, IDummyClassRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public DummyClassRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager) : base(context, httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
        }

        public override Task<List<DummyClass>> GetAllAsync()
        {
            return _context.Set<DummyClass>().Where(e => !e.Eliminado)
                .Include(m => m.DummyClassType)
                .ToListAsync();
        }

        public override async Task<DummyClass> CreateAsync(DummyClass entity)
        {
            entity.ID = await GenerateUniqueIdAsync();
            return await base.CreateAsync(entity);
        }

        public override async Task<List<string>> ValidateModelAsync(DummyClass entity, bool isUpdate)
        {
            // Override this method whit the validations
            var errors = new List<string>();

            errors.AddRange(await ValidateDuplicateAsync<DummyClass>(
                entity,
                entity.Dummy,
                m => m.Dummy == entity.Dummy && !m.Eliminado,
                m => m.ID != entity.ID,
                isUpdate
            ));
            // Write some validations and add errors to list
            return errors;
        }
    }
}
