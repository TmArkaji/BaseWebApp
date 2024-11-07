using BaseWebApplication.Data;
using BaseWebApplication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BaseWebApplication.Repositories
{
    public class DummyClassTypeRepository : GenericRepository<int, DummyClassType>, IDummyClassTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public DummyClassTypeRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager) : base(context, httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
        }
    }
}
