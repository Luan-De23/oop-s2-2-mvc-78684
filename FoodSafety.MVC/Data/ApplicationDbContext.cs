using FoodSafety.Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodSafety.MVC.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Premises> Premises { get; set; } = default!;
    public DbSet<Inspection> Inspections { get; set; } = default!;
    public DbSet<FollowUp>  FollowUps { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Premises>().HasData(
            new Premises
            {
                Id = 1,
                Name = "The Golden Spoon",
                Address = "12 Main St",
                Town = "Cork",
                RiskRating = RiskLevel.High
            },
            new Premises
            {
                Id = 2,
                Name = "Burger Barn",
                Address = "45 Oak Ave",
                Town = "Cork",
                RiskRating = RiskLevel.Medium
            },
            new Premises
            {
                Id = 3,
                Name = "Pizza Palace",
                Address = "78 River Rd",
                Town = "Cork",
                RiskRating = RiskLevel.Low
            },
            new Premises
            { 
                Id = 4,
                Name = "Sushi Stop",
                Address = "23 Elm St", 
                Town = "Cork", 
                RiskRating = RiskLevel.High 
            },
            new Premises
            {
                Id = 5,
                Name = "The Breakfast Club",
                Address = "9 Hill Rd",
                Town = "Dublin",
                RiskRating = RiskLevel.Medium
            },
            new Premises
            {
                Id = 6,
                Name = "Noodle House",
                Address = "34 Lake Dr",
                Town = "Dublin",
                RiskRating = RiskLevel.Low
            },
            new Premises
            {
                Id = 7,
                Name = "Taco Town",
                Address = "56 Park Ln",
                Town = "Dublin",
                RiskRating = RiskLevel.High
            },
            new Premises
            {
                Id = 8,
                Name = "The Green Leaf", 
                Address = "11 Beach Rd",
                Town = "Dublin", 
                RiskRating = RiskLevel.Medium
            },
            new Premises
            {
                Id = 9,
                Name = "Curry Corner", 
                Address = "67 North St",
                Town = "Galway",
                RiskRating = RiskLevel.Low
            },
            new Premises
            {
                Id = 10,
                Name = "Fish & Chips Co",
                Address = "89 West Ave",
                Town = "Galway", 
                RiskRating = RiskLevel.High
            },
            new Premises
            {
                Id = 11,
                Name = "The Sandwich Bar", 
                Address = "14 East Rd", 
                Town = "Galway",
                RiskRating = RiskLevel.Medium
            },
            new Premises
            {
                Id = 12,
                Name = "Sweet Treats", 
                Address = "30 South St",
                Town = "Galway", 
                RiskRating = RiskLevel.Low
            }
            );

        modelBuilder.Entity<Inspection>().HasData(
            new Inspection 
            { 
                Id = 1, 
                PremisesId = 1, 
                InspectionDate = DateOnly.Parse("2026-03-01"), 
                Score = 45, 
                OutCome = Outcomes.Fail, 
                Notes = "Hygiene issues found" 
            },
            new Inspection 
            { 
                Id = 2, 
                PremisesId = 2, 
                InspectionDate = DateOnly.Parse("2026-03-03"), 
                Score = 82, 
                OutCome = Outcomes.Pass, 
                Notes = "Minor issues noted" 
            },
            new Inspection 
            { 
                Id = 3, 
                PremisesId = 3, 
                InspectionDate = DateOnly.Parse("2026-03-05"), 
                Score = 91, 
                OutCome = Outcomes.Pass, 
                Notes = "All clear" 
            },
            new Inspection 
            { 
                Id = 4, 
                PremisesId = 4, 
                InspectionDate = DateOnly.Parse("2026-03-07"), 
                Score = 38, 
                OutCome = Outcomes.Fail, 
                Notes = "Multiple violations" 
            },
            new Inspection 
            { 
                Id = 5, 
                PremisesId = 5, 
                InspectionDate = DateOnly.Parse("2026-03-10"), 
                Score = 74, 
                OutCome = Outcomes.Pass, 
                Notes = "Adequate standards" 
            },
            new Inspection 
            { 
                Id = 6, 
                PremisesId = 6, 
                InspectionDate = DateOnly.Parse("2026-03-12"), 
                Score = 55, 
                OutCome = Outcomes.Fail, 
                Notes = "Storage problems" 
            },
            new Inspection 
            { 
                Id = 7, 
                PremisesId = 7, 
                InspectionDate = DateOnly.Parse("2026-03-15"), 
                Score = 88, 
                OutCome = Outcomes.Pass, 
                Notes = "Good overall" 
            },
            new Inspection 
            { 
                Id = 8, 
                PremisesId = 8, 
                InspectionDate = DateOnly.Parse("2026-03-18"), 
                Score = 62, 
                OutCome = Outcomes.Pass, 
                Notes = "Minor temperature issues" 
            },
            new Inspection 
            { 
                Id = 9, 
                PremisesId = 9, 
                InspectionDate = DateOnly.Parse("2026-02-05"), 
                Score = 41, 
                OutCome = Outcomes.Fail, 
                Notes = "Pest control needed" 
            },
            new Inspection 
            { 
                Id = 10, 
                PremisesId = 10, 
                InspectionDate = DateOnly.Parse("2026-02-10"), 
                Score = 78, 
                OutCome = Outcomes.Pass, 
                Notes = "Satisfactory" 
            },
            new Inspection 
            { 
                Id = 11, 
                PremisesId = 11, 
                InspectionDate = DateOnly.Parse("2026-02-14"), 
                Score = 33, 
                OutCome = Outcomes.Fail, 
                Notes = "Serious violations" 
            },
            new Inspection 
            { 
                Id = 12, 
                PremisesId = 12, 
                InspectionDate = DateOnly.Parse("2026-02-20"), 
                Score = 95, 
                OutCome = Outcomes.Pass, 
                Notes = "Excellent" 
            },
            new Inspection 
            { 
                Id = 13, 
                PremisesId = 1, 
                InspectionDate = DateOnly.Parse("2026-01-08"), 
                Score = 60, 
                OutCome = Outcomes.Pass, 
                Notes = "Improved since last visit" 
            },
            new Inspection 
            { 
                Id = 14, 
                PremisesId = 2, 
                InspectionDate = DateOnly.Parse("2026-01-15"), 
                Score = 49, 
                OutCome = Outcomes.Fail, 
                Notes = "Cross contamination risk" 
            },
            new Inspection 
            { 
                Id = 15, 
                PremisesId = 3, 
                InspectionDate = DateOnly.Parse("2026-01-22"), 
                Score = 85, 
                OutCome = Outcomes.Pass, 
                Notes = "Good hygiene" 
            },
            new Inspection 
            { 
                Id = 16, 
                PremisesId = 4, 
                InspectionDate = DateOnly.Parse("2025-12-03"), 
                Score = 72, 
                OutCome = Outcomes.Pass, 
                Notes = "Acceptable" 
            },
            new Inspection 
            { 
                Id = 17, 
                PremisesId = 5, 
                InspectionDate = DateOnly.Parse("2025-12-10"), 
                Score = 44, 
                OutCome = Outcomes.Fail, 
                Notes = "Temperature violations" 
            },
            new Inspection 
            { 
                Id = 18, 
                PremisesId = 6, 
                InspectionDate = DateOnly.Parse("2025-12-18"), 
                Score = 90, 
                OutCome = Outcomes.Pass, 
                Notes = "Very good" 
            },
            new Inspection 
            { 
                Id = 19, 
                PremisesId = 7, 
                InspectionDate = DateOnly.Parse("2025-11-05"), 
                Score = 37, 
                OutCome = Outcomes.Fail, 
                Notes = "Deep clean required" 
            },
            new Inspection 
            { 
                Id = 20, 
                PremisesId = 8, 
                InspectionDate = DateOnly.Parse("2025-11-12"), 
                Score = 81, 
                OutCome = Outcomes.Pass, 
                Notes = "Well maintained" 
            },
            new Inspection 
            { 
                Id = 21, 
                PremisesId = 9, 
                InspectionDate = DateOnly.Parse("2025-11-20"), 
                Score = 66, 
                OutCome = Outcomes.Pass, 
                Notes = "Minor issues" 
            },
            new Inspection 
            { 
                Id = 22, 
                PremisesId = 10, 
                InspectionDate = DateOnly.Parse("2025-10-08"), 
                Score = 53, 
                OutCome = Outcomes.Fail, 
                Notes = "Drainage problems" 
            },
            new Inspection 
            { 
                Id = 23, 
                PremisesId = 11, 
                InspectionDate = DateOnly.Parse("2025-10-15"), 
                Score = 77, 
                OutCome = Outcomes.Pass, 
                Notes = "Satisfactory" 
            },
            new Inspection 
            { 
                Id = 24, 
                PremisesId = 12, 
                InspectionDate = DateOnly.Parse("2025-10-22"), 
                Score = 29, 
                OutCome = Outcomes.Fail, 
                Notes = "Critical violations" 
            },
            new Inspection 
            { 
                Id = 25, 
                PremisesId = 1, 
                InspectionDate = DateOnly.Parse("2025-09-10"), 
                Score = 88, 
                OutCome = Outcomes.Pass, 
                Notes = "All standards met" 
            }
            );
        
        modelBuilder.Entity<FollowUp>().HasData(
            new FollowUp 
            { 
                Id = 1, 
                InspectionId = 1, 
                DueDate = DateOnly.Parse("2026-02-01"), 
                Status = State.Open, 
                ClosedDate = null 
            },
            new FollowUp 
            { 
                Id = 2, 
                InspectionId = 4, 
                DueDate = DateOnly.Parse("2026-01-15"), 
                Status = State.Open, 
                ClosedDate = null 
            },
            new FollowUp 
            { 
                Id = 3, 
                InspectionId = 9, 
                DueDate = DateOnly.Parse("2026-01-20"), 
                Status = State.Open, 
                ClosedDate = null 
            },
            new FollowUp 
            { 
                Id = 4, 
                InspectionId = 11, 
                DueDate = DateOnly.Parse("2026-02-10"), 
                Status = State.Open, 
                ClosedDate = null 
            },
            new FollowUp 
            { 
                Id = 5, 
                InspectionId = 14, 
                DueDate = DateOnly.Parse("2026-02-28"), 
                Status = State.Open, 
                ClosedDate = null 
            },
            new FollowUp 
            { 
                Id = 6, 
                InspectionId = 17, 
                DueDate = DateOnly.Parse("2025-12-20"), 
                Status = State.Open, 
                ClosedDate = null 
            },
            new FollowUp 
            { 
                Id = 7, 
                InspectionId = 19, 
                DueDate = DateOnly.Parse("2025-12-01"), 
                Status = State.Open, 
                ClosedDate = null 
            },
            new FollowUp 
            { 
                Id = 8, 
                InspectionId = 6, 
                DueDate = DateOnly.Parse("2026-03-10"), 
                Status = State.Closed, 
                ClosedDate = DateOnly.Parse("2026-03-15") 
            },
            new FollowUp 
            { 
                Id = 9, 
                InspectionId = 22, 
                DueDate = DateOnly.Parse("2025-11-01"), 
                Status = State.Closed, 
                ClosedDate = DateOnly.Parse("2025-11-20") 
            },
            new FollowUp 
            { 
                Id = 10, 
                InspectionId = 24, 
                DueDate = DateOnly.Parse("2025-11-15"), 
                Status = State.Closed, 
                ClosedDate = DateOnly.Parse("2025-12-01") 
            }
            );
    }
}

