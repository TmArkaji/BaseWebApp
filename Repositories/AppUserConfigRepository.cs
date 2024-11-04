using BaseWebApplication.Data;
using BaseWebApplication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseWebApplication.Repositories
{
    public class AppUserConfigRepository : GenericRepository<int, AppUserConfig>, IAppUserConfigRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AppUserConfigRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager) : base(context, httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
        }

        public override Task<List<AppUserConfig>> GetAllAsync()
        {
            return _context.Set<AppUserConfig>().Where(e => !e.Eliminado)
                .Include(m => m.AppUser)
                .ToListAsync();
        }
        public override async Task<AppUserConfig> GetByIdAsync(int id)
        {
            return await _context.AppUserConfig.Include(m => m.AppUser).FirstOrDefaultAsync(m => m.ID == id);
        }

        public override async Task<AppUserConfig> UpdateAsync(AppUserConfig entity)
        {
            var model = _context.AppUserConfig.FirstOrDefault(m => m.ID == entity.ID);
            model.UpdateDate = GetDateTime();
            model.UpdateUserId = _userId;


            var user = await _userManager.FindByIdAsync(model.AppUserID);
            if (user != null)
            {
                user.FirstName = entity.AppUser.FirstName;
                user.MiddleName = entity.AppUser.MiddleName;
                user.LastName = entity.AppUser.LastName;
                user.MiddleLastName = entity.AppUser.MiddleLastName;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        // ModelState.AddModelError(string.Empty, error.Description);
                        // Maneja los errores
                    }
                }
                
            }
            
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CreateEmptyConfig(string appUserId)
        {
            var model = new AppUserConfig
            {
                AppUserID = appUserId,
                CreateDate = GetDateTime(),
                CreateUserId = _userId,
                UpdateDate = GetDateTime(),
                UpdateUserId = _userId,
            };

            await _context.AppUserConfig.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.ID;
        }
    }
}
