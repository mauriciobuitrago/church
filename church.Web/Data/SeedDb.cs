using Church.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
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
