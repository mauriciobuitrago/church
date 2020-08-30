using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Church.Common.Entities;
using Church.Web.Data;

namespace Church.Web.Controllers
{
    public class CampusController : Controller
    {
        private readonly DataContext _context;

        public CampusController(DataContext context)
        {
            _context = context;
        }

        // GET: Campus
        public async Task<IActionResult> Index()
        {
            return View(await _context.campuses
                .Include(d => d.Districts)
                .ToListAsync());
        }

        // GET: Campus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.campuses
                .Include(d=>d.Districts)
                .ThenInclude(c=>c.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // GET: Campus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Campus campus)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(campus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }


            }
            return View(campus);
        }

        // GET: Campus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }
            return View(campus);
        }

        // POST: Campus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Campus campus)
        {
            if (id != campus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(campus);
        }


        // GET: Campus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campus campus = await _context.campuses
                .Include(d =>d.Districts)
                .ThenInclude(c =>c.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            try
            {
                _context.campuses.Remove(campus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }



        private bool CampusExists(int id)
        {
            return _context.campuses.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddDistrict(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campus campus = await _context.campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }

            District model = new District { IdCampus = campus.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDistrict(District district)
        {
            if (ModelState.IsValid)
            {
                Campus campus = await _context.campuses
                    .Include(d => d.Districts)
                    .FirstOrDefaultAsync(d => d.Id == district.IdCampus);
                if (campus == null)
                {
                    return NotFound();
                }

                try
                {
                    district.Id = 0;
                    campus.Districts.Add(district);
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{campus.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(district);
        }
        ////////////////////////////////////////////////////////
        public async Task<IActionResult> EditDistrict(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            District district = await _context.districts.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            Campus campus = await _context.campuses.FirstOrDefaultAsync(c => c.Districts.FirstOrDefault(d => d.Id == district.Id) != null);
            district.IdCampus = campus.Id;
            return View(district);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDistrict(District district)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(district);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{district.IdCampus}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(district);
        }


        public async Task<IActionResult> DeleteDistrict(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            District district = await _context.districts
                .Include(c => c.Churches)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            Campus campus = await _context.campuses.FirstOrDefaultAsync(c => c.Districts.FirstOrDefault(d => d.Id == district.Id) != null);
            _context.districts.Remove(district);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{campus.Id}");
        }



    }
}
