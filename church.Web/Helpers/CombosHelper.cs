using Church.Common.Entities;
using Church.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Church.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }


        public IEnumerable<SelectListItem> GetComboCampuses()
        {
            List<SelectListItem> list = _context.campuses.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
        .OrderBy(t => t.Text)
        .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a campus...]",
                Value = "0"
            });

            return list;

        }

        public IEnumerable<SelectListItem> GetComboChurches(int DistrictsId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            District district = _context.districts
                .Include(d => d.Churches)
                .FirstOrDefault(d => d.Id == DistrictsId);
            if (district != null)
            {
                list = district.Churches.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a church...]",
                Value = "0"
            });

            return list;

        }

        public IEnumerable<SelectListItem> GetComboDistricts(int CampusesId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Campus campus = _context.campuses
                .Include(c => c.Districts)
                .FirstOrDefault(c => c.Id == CampusesId);
            if (campus != null)
            {
                list = campus.Districts.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a district...]",
                Value = "0"
            });

            return list;

        }

        public IEnumerable<SelectListItem> GetComboProfessions()
        {
            List<SelectListItem> list = _context.Professions.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
      .OrderBy(t => t.Text)
      .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a profession...]",
                Value = "0"
            });

            return list;
        }
    }
}