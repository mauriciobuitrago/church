using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboProfessions();
        IEnumerable<SelectListItem> GetComboCampuses();

        IEnumerable<SelectListItem> GetComboDistricts(int CampusesId);

        IEnumerable<SelectListItem> GetComboChurches(int DistrictsId);

    }
}
