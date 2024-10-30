using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BaseWebApplication.Configurations.Entities
{
    public class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = Constants.ADMIN_ROLE_ID,
                    Name = Constants.ADMIN_ROLE,
                    NormalizedName = Constants.ADMIN_ROLE.ToUpper(),
                },
                new IdentityRole
                {
                    Id = Constants.GESTOR_ROLE_ID,
                    Name = Constants.GESTOR_ROLE,
                    NormalizedName = Constants.GESTOR_ROLE.ToUpper(),
                }
            );
        }
    }
}
