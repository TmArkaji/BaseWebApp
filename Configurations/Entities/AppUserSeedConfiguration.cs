using BaseWebApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BaseWebApplication.Configurations.Entities
{
    public class AppUserSeedConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();
            builder.HasData(
                new AppUser
                {
                    Id = Constants.DEFAULT_USER_ID,
                    Email = Constants.DEFAULT_USER_EMAIL,
                    UserName = Constants.DEFAULT_USER_EMAIL,
                    NormalizedUserName = Constants.DEFAULT_USER_EMAIL.ToUpper(),
                    NormalizedEmail = Constants.DEFAULT_USER_EMAIL.ToUpper(),
                    primerNombre = "System",
                    segundoNombre = "",
                    primerApellido = "Admin",
                    segundoApellido = "",
                    PasswordHash = hasher.HashPassword(null, "Passw0rd!"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
