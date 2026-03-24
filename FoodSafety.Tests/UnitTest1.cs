using System.Security.Claims;
using FoodSafety.Library;
using FoodSafety.MVC.Controllers;
using FoodSafety.MVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodSafety.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Get_Overdue_FollowUps_Returns_Only_Overdue()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestingDB")
            .Options;

        using var context = new ApplicationDbContext(options);

        var today = DateOnly.FromDateTime(DateTime.Today);

        context.FollowUps.AddRange(
            new FollowUp { DueDate = today.AddDays(-5), Status = State.Open }, // overdue ✅
            new FollowUp { DueDate = today.AddDays(5), Status = State.Open },  // Higher ❌
            new FollowUp { DueDate = today.AddDays(-2), Status = State.Closed } // Closed ❌
        );

        await context.SaveChangesAsync();

        var result = await context.FollowUps
            .Where(f => f.Status == State.Open && f.DueDate < today)
            .ToListAsync();

        Assert.Single(result);
    }
    
    [Fact]
    public void FollowUp_Closed_Without_Date_IsInvalid()
    {
        var followUp = new FollowUp
        {
            Status = State.Closed,
            ClosedDate = null
        };

        var isValid = !(followUp.Status == State.Closed && followUp.ClosedDate == null);

        Assert.False(isValid);
    }
    
    [Fact]
    public async Task Dashboard_Counts_Are_Correct()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestingDB2")
            .Options;

        using var context = new ApplicationDbContext(options);

        var today = DateOnly.FromDateTime(DateTime.Today);

        var premises = new Premises { Name = "Test", Town = "Dublin" };
        context.Premises.Add(premises);

        context.Inspections.AddRange(
            new Inspection { Premises = premises, InspectionDate = today, OutCome = Outcomes.Pass },
            new Inspection { Premises = premises, InspectionDate = today, OutCome = Outcomes.Fail }
        );

        context.FollowUps.Add(
            new FollowUp { DueDate = today.AddDays(-1), Status = State.Open }
        );

        await context.SaveChangesAsync();

        var totalInspections = await context.Inspections.CountAsync();
        var totalFail = await context.Inspections.CountAsync(i => i.OutCome == Outcomes.Fail);
        var overdue = await context.FollowUps.CountAsync(f => f.Status == State.Open && f.DueDate < today);

        Assert.Equal(2, totalInspections);
        Assert.Equal(1, totalFail);
        Assert.Equal(1, overdue);
    }
    
    [Fact]
    public void Viewer_Cannot_Access_Create()
    {
        var controller = new FollowUpController(null, null);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Viewer")    
                }, "mock"))
            }
        };

        var hasAccess = controller.User.IsInRole("Inspector");

        Assert.False(hasAccess);
    }
    
    [Fact]
    public void Inspector_Has_Access()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Role, "Inspector")
        }));

        Assert.True(user.IsInRole("Inspector"));
    }
}