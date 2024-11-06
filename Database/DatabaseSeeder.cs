using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Database
{
    public static class DatabaseSeeder
    {
        public async static void Seed(this DatabaseContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new IdentityRole[]
                    {
                    new IdentityRole
                    {
                        Id = "1D5CAA3C-6A65-4376-9A7A-8A082EA143F7",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "1abdecf9-b8d5-4f46-9a28-d51782ed0ea2"
                    },
                    new IdentityRole
                    {
                        Id = "2D5CAA3C-6A65-4376-9A7A-8A082EA143F7",
                        Name = "Applicant",
                        NormalizedName = "APPLICANT",
                        ConcurrencyStamp = "2abdecf9-b8d5-4f46-9a28-d51782ed0ea2"
                    },
                    new IdentityRole
                    {
                        Id = "3D5CAA3C-6A65-4376-9A7A-8A082EA143F7",
                        Name = "Employer",
                        NormalizedName = "EMPLOYER",
                        ConcurrencyStamp = "3abdecf9-b8d5-4f46-9a28-d51782ed0ea2"
                    }
                });
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(new User[]
                {
                    new()
                    {
                        Id = "7BB8A621-4BAF-4536-A642-4DFC855236EA",
                        Role = Role.Admin,
                        FullName = "Адміністратор",
                        Bio = "Admin",
                        Sector = "Admin",
                        EmployeesCount = "Admin",
                        Organisation = "Admins",
                        UserName = "admin@admin.com",
                        NormalizedUserName = "ADMIN@ADMIN.COM",
                        Email = "admin@admin.com",
                        NormalizedEmail = "ADMIN@ADMIN>COM",
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEMESxjSx1//JBXQ/b5YfGgCkKg3nqKx7YupycmH2iPjYCtx1K1SojyGVt3vtT6ikaw==",
                        SecurityStamp = "XRET4R3X4CFGFTDPSNRACQ7W3AFT5W6W",
                        ConcurrencyStamp = "9bf73813-41a7-41d1-8f86-3b49f0384a85",
                        PhoneNumber = "0000000000",
                        PhoneNumberConfirmed = true,
                        Resume = new Resume()
                        {
                            Id = "AECD3E45-CD20-4AB8-87E4-D7F86079687C",
                            UserId = "7BB8A621-4BAF-4536-A642-4DFC855236EA"
                        }
                    }
                });

                if (!context.UserRoles.Any())
                {
                    context.UserRoles.AddRange(new IdentityUserRole<string>[]
                    {
                        new IdentityUserRole<string>
                        {
                            UserId = "7BB8A621-4BAF-4536-A642-4DFC855236EA",
                            RoleId = "1D5CAA3C-6A65-4376-9A7A-8A082EA143F7"
                        }
                    });
                }

                if (!context.Vacancies.Any())
                {

                }

                if (!context.Applications.Any())
                {

                }
            }
            await context.SaveChangesAsync();
        }
    }
}
