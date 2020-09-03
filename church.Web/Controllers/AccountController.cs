using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Church.Common.Entities;
using Church.Common.Enums;
using Church.Web.Data;
using Church.Web.Data.Entities;
using Church.Web.Helpers;
using Church.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Church.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICombosHelper _combosHelper;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
       
        private readonly IBlobHelper _blobHelper;

        public AccountController(
        ICombosHelper combosHelper,
        DataContext context,
        IUserHelper userHelper, 
        IBlobHelper blobHelper
)
        {
            _combosHelper = combosHelper;
            _context = context;
            _userHelper = userHelper;
           
            _blobHelper = blobHelper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email or password incorrect.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Campuses = _combosHelper.GetComboCampuses(),
                Districts = _combosHelper.GetComboDistricts(0),
                Churchis = _combosHelper.GetComboChurches(0),
                Professions =_combosHelper.GetComboProfessions()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _userHelper.AddUserAsync(model, imageId, UserType.User);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    model.Campuses = _combosHelper.GetComboCampuses();
                    model.Districts = _combosHelper.GetComboDistricts(model.CampusesId);
                    model.Churchis = _combosHelper.GetComboChurches(model.DistrictsId);
                    model.Professions = _combosHelper.GetComboProfessions();
                    return View(model);
                }

                LoginViewModel loginViewModel = new LoginViewModel
                {
                    Password = model.Password,
                    RememberMe = false,
                    Username = model.Username
                };

                var result2 = await _userHelper.LoginAsync(loginViewModel);

                if (result2.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            model.Campuses = _combosHelper.GetComboCampuses();
            model.Districts = _combosHelper.GetComboDistricts(model.CampusesId);
            model.Churchis = _combosHelper.GetComboChurches(model.DistrictsId);
            model.Professions = _combosHelper.GetComboProfessions();
            return View(model);
        }


        public JsonResult GetDistricts(int campusesId)
        {
            Campus campus = _context.campuses
                .Include(c => c.Districts)
                .FirstOrDefault(c => c.Id == campusesId);
            if (campus == null)
            {
                return null;
            }

            return Json(campus.Districts.OrderBy(d => d.Name));
        }

        public JsonResult GetChurch(int DistrictsId)
        {
            District district = _context.districts
                .Include(d => d.Churches)
                .FirstOrDefault(d => d.Id == DistrictsId);
            if (district == null)
            {
                return null;
            }

            return Json(district.Churches.OrderBy(c => c.Name));
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }


    }

}
