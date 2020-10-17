using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Church.Common.Entities;
using Church.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Vereyon.Web;

namespace Church.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CampusController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public CampusController(DataContext context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
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


                _context.campuses.Remove(campus);
                await _context.SaveChangesAsync();

            try
            {
                _context.campuses.Remove(campus);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("The camps was deleted.");
            }
            catch
            {
                _flashMessage.Danger("The camps can't be deleted because it has related records.");
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

        ///////////////////////////////////////////////////
        ///

        public async Task<IActionResult> DetailsDistrict(int? id)
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
            district.IdCampus = campus.Id;
            return View(district);
        }


        ////////////////////////////////////////////////
        ///

        public async Task<IActionResult> AddChurch(int? id)
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

            Churchi model = new Churchi{ IdChurch = district.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChurch(Churchi churchi)
        {
            if (ModelState.IsValid)
            {
                District district= await _context.districts
                    .Include(c => c.Churches)
                    .FirstOrDefaultAsync(c => c.Id == churchi.IdChurch);
                if (district == null)
                {
                    return NotFound();
                }

                try
                {
                    churchi.Id = 0;
                    district.Churches.Add(churchi);
                    _context.Update(district);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsDistrict)}/{district.Id}");

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

            return View(churchi);
        }


        public async Task<IActionResult> EditChurch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Churchi churchi = await _context.churches.FindAsync(id);
            if (churchi == null)
            {
                return NotFound();
            }

            District district = await _context.districts.FirstOrDefaultAsync(d => d.Churches.FirstOrDefault(c => c.Id == churchi.Id) != null);
            churchi.IdChurch = district.Id;
            return View(churchi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditChurch(Churchi churchi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(churchi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsDistrict)}/{churchi.IdChurch}");

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
            return View(churchi);
        }

        public async Task<IActionResult> DeleteChurch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Churchi churchi= await _context.churches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (churchi == null)
            {
                return NotFound();
            }

            District district= await _context.districts.FirstOrDefaultAsync(d => d.Churches.FirstOrDefault(c => c.Id == churchi.Id) != null);
            _context.churches.Remove(churchi);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsDistrict)}/{district.Id}");
        }

        public async Task<IActionResult> IndexUser()
        {
            return View(await _context.Users.ToListAsync());
        }

    }
}
