using FoodSafety.Library;
using FoodSafety.MVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodSafety.MVC.Controllers;

[Authorize(Roles = "Admin,Inspector,Viewer")]
public class DashboardController : Controller
{
    
    private readonly ILogger<DashboardController> _logger;
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context,  ILogger<DashboardController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IActionResult> Index(string SelectedTown,  string SelectedRiskRating)
    {
         
        IQueryable<Premises> premisesQuery = _context.Premises;
        
        if (!string.IsNullOrEmpty(SelectedTown))
        {
            premisesQuery = premisesQuery.Where(p => p.Town == SelectedTown);
        }
    
        if (!string.IsNullOrEmpty(SelectedRiskRating) && Enum.TryParse<RiskLevel>(SelectedRiskRating, out var risk))
        {
            premisesQuery = premisesQuery.Where(p => p.RiskRating == risk);
        }
        
        var premisesIds = await premisesQuery.Select(p => p.Id).ToListAsync();
        
        ViewBag.TownList = new SelectList(await _context.Premises.Select(p => p.Town).Distinct().ToListAsync(), SelectedTown);
        ViewBag.RiskList = new SelectList(Enum.GetValues(typeof(RiskLevel)), SelectedRiskRating);
        
        ViewData["CurrentTown"] = SelectedTown;
        ViewData["CurrentRisk"] = SelectedRiskRating;
        
        var dashboard = new ViewModel();

        // Load
        dashboard.TotalPremises = premisesIds.Count;

        // Search the Inspection Table 
        var thisMonth = DateOnly.FromDateTime(DateTime.Today);
        dashboard.TotalInspections = await _context.Inspections
        .Where(i => premisesIds.Contains(i.PremisesId) &&
                i.InspectionDate.Month == thisMonth.Month && i.InspectionDate.Year == thisMonth.Year)
        .CountAsync();

        dashboard.TotalFail = await _context.Inspections
            .Where(i => premisesIds.Contains(i.PremisesId) &&
                        i.OutCome == Outcomes.Fail && i.InspectionDate.Month == thisMonth.Month && i.InspectionDate.Year == thisMonth.Year)
            .CountAsync();
        
        dashboard.AllInspections = await _context.Inspections
            .Where(i => premisesIds.Contains(i.PremisesId))
            .Include(i => i.Premises)
            .OrderByDescending(i => i.Id)
            .ToListAsync();
        

        // search fo overdue followUps
        dashboard.OverdueFollowUps = await _context.FollowUps
            .Where(f => premisesIds.Contains(f.Inspection.Premises.Id) &&
                        f.Status == State.Open && f.DueDate < thisMonth)
            .CountAsync();

        
        return View(dashboard);
    }
}