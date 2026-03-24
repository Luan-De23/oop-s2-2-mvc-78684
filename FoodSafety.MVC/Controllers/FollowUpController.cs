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
    public class FollowUpController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FollowUpController> _logger;

        public FollowUpController(ApplicationDbContext context,  ILogger<FollowUpController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: FollowUp
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FollowUps.Include(f => f.Inspection);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FollowUp/Details/5
        [Authorize(Roles = "Admin,Inspector,Viewer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUps
                .Include(f => f.Inspection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }

        // GET: FollowUp/Create
        public IActionResult Create()
        {
            ViewData["InspectionId"] = new SelectList(_context.Inspections, "Id", "Notes");
            return View();
        }

        // POST: FollowUp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Inspector")]
        public async Task<IActionResult> Create([Bind("Id,DueDate,Status,ClosedDate,InspectionId")] FollowUp followUp)
        {
            try
            {
                var inspections =  await _context.Inspections
                    .FirstOrDefaultAsync(i => i.Id == followUp.InspectionId);
            
                if (inspections != null && inspections.InspectionDate > followUp.DueDate)
                {
                    ModelState.AddModelError("DueDate","Invalid entry");
                    _logger.LogWarning("FollowUp DueDate {DueDate} is before InspectionDate {InspectionDate} by {User}", followUp.DueDate, inspections.InspectionDate, User.Identity.Name ?? "Anonymous");
                }
                if (ModelState.IsValid)
                {
                    _context.Add(followUp);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("FollowUp created: {FollowUpId} for InspectionId {InspectionId} by {User}");
                    return RedirectToAction(nameof(Index));
                }
                ViewData["InspectionId"] = new SelectList(_context.Inspections, "Id", "Notes", followUp.InspectionId);
                return View(followUp);
            }
            catch (Exception e)
            {
                        _logger.LogError("Unhandled exception occurred: {Message} by {User}", e.Message, User.Identity?.Name ?? "Anonymous");
                throw;
            }
            
        }

        // GET: FollowUp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUps.FindAsync(id);
            if (followUp == null)
            {
                return NotFound();
            }
            ViewData["InspectionId"] = new SelectList(_context.Inspections, "Id", "Notes", followUp.InspectionId);
            return View(followUp);
        }

        // POST: FollowUp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Inspector")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DueDate,Status,ClosedDate,InspectionId")] FollowUp followUp)
        {
            try
            {
                if (id != followUp.Id)
                {
                    return NotFound();
                }
            
                var todayDate = DateOnly.FromDateTime(DateTime.Today);
                if (followUp.Status == State.Closed)
                {
                    followUp.ClosedDate = todayDate;
                    _logger.LogInformation("FollowUp closed: {todayDate} by {User}", todayDate,  User.Identity.Name ?? "Anonymous" );
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(followUp);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("FollowUp closed: {FollowUpId} on {ClosedDate} by {User}", followUp.Id, followUp.ClosedDate, User.Identity?.Name ?? "Anonymous");
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        if (!FollowUpExists(followUp.Id))
                        {
                            _logger.LogError("Exception in {Action}: {Message}", nameof(Edit), e.Message);
                            return NotFound();
                        }
                        else
                        {
                            _logger.LogError("Exception in {Action}: {Message}");
                            throw;
                        }
                    
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["InspectionId"] = new SelectList(_context.Inspections, "Id", "Notes", followUp.InspectionId);
                return View(followUp);
            }
            catch (Exception e)
            {
                _logger.LogError("Unhandled exception occurred: {Message} by {User}", e.Message, User.Identity?.Name ?? "Anonymous");
                throw;
            }
        }

        // GET: FollowUp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUps
                .Include(f => f.Inspection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }

        // POST: FollowUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var followUp = await _context.FollowUps.FindAsync(id);
            if (followUp != null)
            {
                _context.FollowUps.Remove(followUp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowUpExists(int id)
        {
            return _context.FollowUps.Any(e => e.Id == id);
        }
    }
}
