using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodSafety.Library;
using FoodSafety.MVC.Data;
using Microsoft.AspNetCore.Authorization;

namespace FoodSafety.MVC.Controllers
{
    public class InspectionController : Controller
    {
        private readonly ILogger<InspectionController> _logger;
        private readonly ApplicationDbContext _context;

        public InspectionController(ApplicationDbContext context,  ILogger<InspectionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Inspection
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inspections.Include(i => i.Premises);
            return View(await applicationDbContext.ToListAsync());
        }
        
        // GET: Inspection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Premises)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // GET: Inspection/Create
        [Authorize(Roles = "Admin,Inspector")]
        public IActionResult Create()
        {
            ViewData["PremisesId"] = new SelectList(_context.Premises, "Id", "Address");
            return View();
        }

        // POST: Inspection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Inspector")]
        public async Task<IActionResult> Create([Bind("Id,InspectionDate,Score,OutCome,Notes,PremisesId")] Inspection inspection)
        {
            try
            {
                if (inspection.Score < 0 || inspection.Score > 100)
                {
                    ModelState.AddModelError("Score", "Score must be between 0 and 100");
                }
                if (ModelState.IsValid)
                {
                    _context.Add(inspection);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Inspection created: {InspectionId} for PremisesId {PremisesId} by {User}", inspection.Id, inspection.PremisesId, User.Identity?.Name ?? "Anonymous");
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PremisesId"] = new SelectList(_context.Premises, "Id", "Address", inspection.PremisesId);
                return View(inspection);
            }
            catch (Exception e)
            {
                _logger.LogError("Unhandled exception occurred: {Message} by {User}", e.Message, User.Identity?.Name ?? "Anonymous");
                throw;
            }
        }

        // GET: Inspection/Edit/5
        [Authorize(Roles = "Admin,Inspector")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }
            ViewData["PremisesId"] = new SelectList(_context.Premises, "Id", "Address", inspection.PremisesId);
            return View(inspection);
        }

        // POST: Inspection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Inspector")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InspectionDate,Score,OutCome,Notes,PremisesId")] Inspection inspection)
        {
            try
            {
                if (id != inspection.Id)
                {
                    return NotFound();
                }
                if (inspection.Score < 0 || inspection.Score > 100)
                {
                    ModelState.AddModelError("Score", "Score must be between 0 and 100");
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(inspection);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Invalid score {Score} attempted for PremisesId {PremisesId} by {User}", inspection.Score, inspection.PremisesId, User.Identity?.Name ?? "Anonymous");
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!InspectionExists(inspection.Id))
                        {
                            _logger.LogError("Exception in {Action}: {Message}", nameof(Edit), e.Message);
                            return NotFound();
                        }
                        else
                        {
                            _logger.LogError("Exception in {Action}: {Message}", nameof(Edit), e.Message);
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PremisesId"] = new SelectList(_context.Premises, "Id", "Address", inspection.PremisesId);
                return View(inspection);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Inspection updated: {InspectionId} for PremisesId {PremisesId} by {User}", inspection.Id, inspection.PremisesId, User.Identity?.Name ?? "Anonymous");
                throw;
            }
            
        }

        // GET: Inspection/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections
                .Include(i => i.Premises)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inspection == null)
            {
                return NotFound();
            }

            return View(inspection);
        }

        // POST: Inspection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection != null)
            {
                _context.Inspections.Remove(inspection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InspectionExists(int id)
        {
            return _context.Inspections.Any(e => e.Id == id);
        }
    }
}
