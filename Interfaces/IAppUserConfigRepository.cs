using BaseWebApplication.Data;

namespace BaseWebApplication.Interfaces
{
    public interface IAppUserConfigRepository : IGenericRepository<int, AppUserConfig>
    {
        public Task<int> CreateEmptyConfig(string appUserId);
    }
}
