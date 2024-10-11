using Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.EF.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedOn)
                .IsRequired(true);

            builder.Property(x => x.FirstName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.HasData(GetUsersDefault());
        }

        public static User[] GetUsersDefault() => new[]
        {
            new User
            {
                FirstName = "Juan",
                LastName = "Salvo",
                CreatedOn = DateTime.Now,
                UserName = "juan@salvo.com",
                NormalizedUserName = "JUAN@SALVO.COM",
                Email = "juan@salvo.com",
                NormalizedEmail = "JUAN@SALVO.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEOil0PeWmcDSO+Dc8kDEdi3x3FiDDXZD1p/ogWEn2wfgyvSSiuft8K+AW1I7MZBCOw==", //asdf123
                SecurityStamp = "JTPW4E432HW3NINUZXHV7YR7T2N5GISU",
                ConcurrencyStamp = "ee45e2d7-9ab6-4a06-ac8b-973be5f8b27d",
                LockoutEnd = DateTimeOffset.Now,
                LockoutEnabled = true
            }
        };
    }
}
