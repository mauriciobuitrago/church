using Church.Common.Entities;
using Church.Common.Enums;
using Church.Web.Data.Entities;
using Church.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;

        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("mauro", "buitrago", "1152","Calle Luna Calle Sol","322 311 4620","maob14@gmail.com", UserType.Admin);

        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(
            string firstName,
            string lastName,
            string document,
            string address,
            string PhoneNumber,
            string email,
            
            
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Document = document,
                    Address = address,
                    NumberPhone = PhoneNumber,
                    Email = email,
                    Churchi = _context.churches.FirstOrDefault(),
                    Profession = _context.Professions.FirstOrDefault(),
                    UserName = email,
                                                       
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.campuses.Any())
            {
                _context.campuses.Add(new Campus
                {
                    Name = "Antioquia",
                    Districts = new List<District>
                {
                    new District
                    {
                        Name = "Medellin",
                        Churches = new List<Churchi>
                        {
                            new Churchi { Name = "iglesia catolica dulce jesus mio" },
                            new Churchi { Name = "Los caminos del señor pentecostal" },
                            new Churchi { Name = "los cristianos del mas allá" }
                        }
                    },
                    new District
                    {
                        Name = "Bello",
                        Churches= new List<Churchi>
                        {
                            new Churchi { Name = "parroquia san atanasio" },
                            new Churchi { Name = "los poderosos" },
                            new Churchi { Name = "Atalanta y sus deseos del señor" }
                        }
                    },
                    new District
                    {
                        Name = "Itagui",
                        Churches = new List<Churchi>
                        {
                            new Churchi { Name = "Profesia" },
                            new Churchi { Name = "el jardin del eden" },
                            new Churchi { Name = "el mas allá" }
                        }
                    }
                }
                });
                _context.campuses.Add(new Campus
                {
                    Name = "Bogota",
                    Districts = new List<District>
                {
                    new District
                    {
                        Name = "Suba",
                        Churches = new List<Churchi>
                        {
                            new Churchi { Name = "Los Angeles de Dios" },
                            new Churchi { Name = "Parroquia Suba" },
                            new Churchi { Name = "Santo Tomas" }
                        }
                    },
                    new District
                    {
                        Name = "Engativa",
                        Churches = new List<Churchi>
                        {
                            new Churchi { Name = "Parroquia del destino" },
                            new Churchi { Name = "Los Jesuitas" }
                        }
                    }
                }
                });
                await _context.SaveChangesAsync();
            }
        }
    }

}
