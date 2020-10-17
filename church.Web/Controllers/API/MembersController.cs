using Church.Common.Entities;
using Church.Common.Enums;

using Church.Web.Data;
using Church.Web.Data.Entities;
using Church.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onsale.Common.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public MembersController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            List<User> members = await _context.Users
                .Include(t => t.Churchi)
                .ThenInclude(d => d.District)
                .ThenInclude(c => c.Campuses)
                .Include(t => t.Profession)
                .Where(u => u.UserType == UserType.Member)
                .ToListAsync();

            return Ok(members);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("GetMembersByChurch")]
        public async Task<IActionResult> GetMembersByChurch([FromBody] EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = await _userHelper.GetUserAsync(request.Email);
            Churchi idChurchi = user.Churchi;

            List<User> members = await _context.Users
                .Where(u => u.Churchi == idChurchi)
                .Where(u => u.UserType == UserType.Member)
                .ToListAsync();

            return Ok(members);
        }
    }
}