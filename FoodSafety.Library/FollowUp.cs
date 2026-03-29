using System.ComponentModel.DataAnnotations;

namespace FoodSafety.Library;

public enum State
{
    Open,
    Closed,
}

public class FollowUp
{
    public int Id { get; set; }
    [Display(Name = "Due Date")]
    public DateOnly DueDate { get; set; }
    public State Status { get; set; }
    [Display(Name = "Closed Date")]
    public DateOnly? ClosedDate { get; set; }
    
    
    //PLK's
    public int InspectionId { get; set; }
    public Inspection? Inspection { get; set; }
    
}