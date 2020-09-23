using Church.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampusController : ControllerBase
    {
          
        
            private readonly DataContext _context;

            public CampusController(DataContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult GetCampus()
            {
                return Ok(_context.campuses
                    .Include(c => c.Districts)
                    .ThenInclude(d => d.Churches));
            }
        

    }
}
