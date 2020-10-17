using Church.Web.Data;
using Church.Common.Enums;

using Church.Web.Data.Entities;
using Church.Web.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Church.Web.Controllers
{
    public class UsersListController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public UsersListController(DataContext context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Where(u => u.UserType != 0)
                .Where(u => u.UserType == UserType.Member)
                .Include(p => p.Profession)
                .Include(t => t.Churchi)
                .ThenInclude(d => d.District)
                .ThenInclude(c => c.Campuses)
                .OrderBy(n => n.FullName)
                .ToListAsync());
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await _context.Users
                 .Include(p => p.Profession)
                .Include(t => t.Churchi)
                .ThenInclude(d => d.District)
                .ThenInclude(c => c.Campuses)
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("The users was deleted.");
            }
            catch
            {
                _flashMessage.Danger("The users can't be deleted because it has related records.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}