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
    [Authorize(Roles = "Admin,Inspector")]
    public class PremisesController : Controller
    {
        private readonly ILogger<PremisesController> _logger;
        private readonly ApplicationDbContext _context;

        public PremisesController(ApplicationDbContext context,  ILogger<PremisesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Premises
        public async Task<IActionResult> Index()
        {
            return View(await _context.Premises.ToListAsync());
        }

        // GET: Premises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premises = await _context.Premises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premises == null)
            {
                return NotFound();
            }

            return View(premises);
        }

        // GET: Premises/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Premises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Town,RiskRating")] Premises premises)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(premises);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Premises created: {PremisesId} {Name} by {User}", premises.Id, premises.Name, User.Identity?.Name ?? "Anonymous");
                    return RedirectToAction(nameof(Index));
                }
                return View(premises);
            }
            catch (Exception e)
            {
                _logger.LogError("Unhandled exception occurred: {Message} by {User}", e.Message, User.Identity?.Name ?? "Anonymous");
                throw;
            }
        }
        // GET: Premises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premises = await _context.Premises.FindAsync(id);
            if (premises == null)
            {
                return NotFound();
            }
            return View(premises);
        }

        // POST: Premises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Town,RiskRating")] Premises premises)
        {
            try
            {
                if (id != premises.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(premises);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Premises updated: {PremisesId} by {User}", premises.Id, User.Identity?.Name ?? "Anonymous");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        if (!PremisesExists(premises.Id))
                        {
                            _logger.LogError("Exception in {Action}: {Message}", nameof(Edit), ex.Message);
                            return NotFound();
                        }
                        else
                        {
                            _logger.LogError("Exception in {Action}: {Message}", nameof(Edit), ex.Message);
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(premises);
            }
            catch (Exception e)
            {
                _logger.LogError("Unhandled exception occurred: {Message} by {User}", e.Message, User.Identity?.Name ?? "Anonymous");
                throw;
            }
            
        }

        // GET: Premises/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premises = await _context.Premises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premises == null)
            {
                return NotFound();
            }

            return View(premises);
        }

        // POST: Premises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var premises = await _context.Premises.FindAsync(id);
            if (premises != null)
            {
                _context.Premises.Remove(premises);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PremisesExists(int id)
        {
            return _context.Premises.Any(e => e.Id == id);
        }
    }
}
