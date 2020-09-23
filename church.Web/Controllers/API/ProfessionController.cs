using Church.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionController : ControllerBase
    {
        private readonly DataContext _context;

        public ProfessionController (DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProfession()
        {
            return Ok(_context.Professions);
        }
    }

}
