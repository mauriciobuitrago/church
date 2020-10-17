using Church.Common.Entities;
using Church.Common.Enums;
using Church.Web.Data;
using Church.Web.Data.Entities;
using Church.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;

        public MembersController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            _mailHelper = mailHelper;
        }


        public async Task<IActionResult> Index()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            Churchi idChurchi= user.Churchi;


            return View(await _context.Users
                .Where(u => u.Churchi == idChurchi)
                .Where(u => u.UserType == UserType.Member)
                .Include(p => p.Profession)
                .Include(t => t.Churchi)
                .ThenInclude(d => d.District)
                .ThenInclude(c => c.Campuses)
                .OrderBy(n => n.FullName)
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            List<User> members = await _context.Users
                .Where(u => u.UserType == UserType.Member)
                .ToListAsync();

            return Ok(members);
        }



    }
}