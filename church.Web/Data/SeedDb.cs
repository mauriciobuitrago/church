using Church.Common.Entities;
using Church.Common.Enums;
using Church.Common.Models;
using Church.Common.Services;
using Church.Web.Data.Entities;
using Church.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IApiService _apiService;
        private readonly Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper, IApiService apiService)
        {
         
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _apiService = apiService;
            _random = new Random();

        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckProfessionsAsync();
            await CheckRolesAsync();
            await CheckUsersAsync();
            

        }
        private async Task CheckUsersAsync()
        {
            if (!_context.Users.Any())
            {
                await CheckAdminsAsync();
                await CheckMemberAsync();
                await CheckTeacherAsync();
              

            }
        }


     

        private async Task CheckMemberAsync()
        {

            await CheckUserAsync($"345001", $"member1@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "iglesia catolica dulce jesus mio"), UserType.Member);
            await CheckUserAsync($"1234002", $"member2@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "iglesia catolica dulce jesus mio"), UserType.Member);
            await CheckUserAsync($"11234123003", $"member3@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "iglesia catolica dulce jesus mio"), UserType.Member);
            await CheckUserAsync($"1123004", $"member4@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "iglesia catolica dulce jesus mio"), UserType.Member);
            await CheckUserAsync($"15324005", $"member5@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "iglesia catolica dulce jesus mio"), UserType.Member);

            await CheckUserAsync($"153245006", $"member6@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Los caminos del señor pentecostal"), UserType.Member);
            await CheckUserAsync($"11234123007", $"member7@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Los caminos del señor pentecostal"), UserType.Member);
            await CheckUserAsync($"1001234128", $"member8@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Los caminos del señor pentecostal"), UserType.Member);
            await CheckUserAsync($"10123412309", $"member9@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Los caminos del señor pentecostal"), UserType.Member);
            await CheckUserAsync($"101234213410", $"member10@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Los caminos del señor pentecostal"), UserType.Member);

            await CheckUserAsync($"10123412311", $"member11@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "los cristianos del mas allá"), UserType.Member);
            await CheckUserAsync($"101234123412", $"member12@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "los cristianos del mas allá"), UserType.Member);
            await CheckUserAsync($"10123412313", $"member13@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "los cristianos del mas allá"), UserType.Member);
            await CheckUserAsync($"1013242314", $"member14@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "los cristianos del mas allá"), UserType.Member);
            await CheckUserAsync($"10123412315", $"member15@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "los cristianos del mas allá"), UserType.Member);

            await CheckUserAsync($"10123416", $"member16@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "parroquia san atanasio"), UserType.Member);
            await CheckUserAsync($"1012317", $"member17@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "parroquia san atanasio"), UserType.Member);
            await CheckUserAsync($"10123418", $"member18@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "parroquia san atanasio"), UserType.Member);
            await CheckUserAsync($"10132419", $"member19@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "parroquia san atanasio"), UserType.Member);
            await CheckUserAsync($"10123420", $"member20@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "parroquia san atanasio"), UserType.Member);

            await CheckUserAsync($"10421321", $"member21@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Atalanta y sus deseos del señor"), UserType.Member);
            await CheckUserAsync($"10123422", $"member22@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Atalanta y sus deseos del señor"), UserType.Member);
            await CheckUserAsync($"1024323", $"member23@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Atalanta y sus deseos del señor"), UserType.Member);
            await CheckUserAsync($"1043224", $"member24@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Atalanta y sus deseos del señor"), UserType.Member);
            await CheckUserAsync($"11234025", $"member25@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Atalanta y sus deseos del señor"), UserType.Member);

            await CheckUserAsync($"10412326", $"member26@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Profesia"), UserType.Member);
            await CheckUserAsync($"1412213027", $"member27@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Profesia"), UserType.Member);
            await CheckUserAsync($"11234028", $"member28@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Profesia"), UserType.Member);
            await CheckUserAsync($"10123429", $"member29@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Profesia"), UserType.Member);
            await CheckUserAsync($"10321430", $"member30@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Profesia"), UserType.Member);

            await CheckUserAsync($"10123431", $"member31@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Santo Tomas"), UserType.Member);
            await CheckUserAsync($"10123432", $"member32@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Santo Tomas"), UserType.Member);
            await CheckUserAsync($"1042333", $"member33@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Santo Tomas"), UserType.Member);
            await CheckUserAsync($"10123434", $"member34@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Santo Tomas"), UserType.Member);
            await CheckUserAsync($"1043211235", $"member35@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Santo Tomas"), UserType.Member);

            await CheckUserAsync($"10123436", $"member36@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Parroquia Suba"), UserType.Member);
            await CheckUserAsync($"10413237", $"member37@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Parroquia Suba"), UserType.Member);
            await CheckUserAsync($"10123438", $"member38@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Parroquia Suba"), UserType.Member);
            await CheckUserAsync($"1041239", $"member39@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Parroquia Suba"), UserType.Member);
            await CheckUserAsync($"10123440", $"member40@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Parroquia Suba"), UserType.Member);

        }

        private async Task CheckAdminsAsync()
        {
            await CheckUserAsync("0001", "maob14@gmail.com", null, UserType.Admin);
        }


        private async Task CheckTeacherAsync()
        {

            await CheckUserAsync($"3123001", $"teacher1@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "iglesia catolica dulce jesus mio"), UserType.Teacher);
            await CheckUserAsync($"12312002", $"teacher2@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Los caminos del señor pentecostal"), UserType.Teacher);
            await CheckUserAsync($"1233003", $"teacher3@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Atalanta y sus deseos del señor"), UserType.Teacher);
            await CheckUserAsync($"1233004", $"teacher4@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "parroquia san atanasio"), UserType.Teacher);
            await CheckUserAsync($"3123005", $"teacher5@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Profesia"), UserType.Teacher);
            await CheckUserAsync($"4233006", $"teacher6@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Santo Tomas"), UserType.Teacher);
            await CheckUserAsync($"2343007", $"teacher7@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "Parroquia Suba"), UserType.Teacher);
            await CheckUserAsync($"1233008", $"teacher8@yopmail.com", await _context.churches.FirstOrDefaultAsync(c => c.Name == "los cristianos del mas allá"), UserType.Teacher);



        }


        private async Task<User> CheckUserAsync(
             string document,
             string email,
             Churchi churchi,
             UserType userType
             )

        {
            RandomUsers randomUsers;

            do
            {
                randomUsers = await _apiService.GetRandomUser("https://randomuser.me", "api");
            } while (randomUsers == null);

            Guid imageId = Guid.Empty;
            RandomUser randomUser = randomUsers.Results.FirstOrDefault();
            string imageUrl = randomUser.Picture.Large.ToString().Substring(22);
            Stream stream = await _apiService.GetPictureAsync("https://randomuser.me", imageUrl);

            if (stream != null)
            {
                imageId = await _blobHelper.UploadBlobAsync(stream, "users");
            }

            int templeId = _random.Next(1, _context.churches.Count());
            int randomTemple = _random.Next(1, _context.churches.Count());
            int randomProfession = _random.Next(1, _context.Professions.Count());

            Profession profession = _context.Professions.FirstOrDefault(t => t.Id == randomProfession);
            User user = await _userHelper.GetUserAsync(email);

            if (user == null)
            {
                user = new User
                {
                    FirstName = randomUser.Name.First,
                    LastName = randomUser.Name.Last,
                    Document = document,
                    Address = $"{randomUser.Location.Street.Number}, {randomUser.Location.Street.Name}",
                    NumberPhone = randomUser.Cell,
                    Email = email,
                    UserName = email,
                    Churchi = churchi,
                    Profession = profession,
                    UserType = userType,
                    ImageId = imageId

                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

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


        private async Task CheckProfessionsAsync()
        {
            if (!_context.Professions.Any())
            {
                _context.Professions.Add(new Profession
                {
                    Name = "Desarrollador",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Analista de QA",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Automatizador",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Policia",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Chofer",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Actor",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Ingeniero civil",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Contador publico",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Dentista",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Doctor",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Profesor",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Empleada de oficios varios",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Diseñador grafico",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Barbero",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Analista biologo",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Peluquero",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Oftalmólogo",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Confesiones",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Militar",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Taxista",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Comerciante",
                });
                _context.Professions.Add(new Profession
                {
                    Name = "Silicato",
                });

            }
            await _context.SaveChangesAsync();
        }
        private class Information
        {
            public string Firstname { get; set; }

            public string Lastname { get; set; }

            public string Image { get; set; }

            public UserType UserType { get; set; }
        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Member.ToString());
            await _userHelper.CheckRoleAsync(UserType.Teacher.ToString());
        }

    }



}
