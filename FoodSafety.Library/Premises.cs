using System.ComponentModel.DataAnnotations;

namespace FoodSafety.Library;

public enum RiskLevel
{
    Low,
    Medium,
    High
}

public class Premises
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string Town { get; set; } = "";
    [Display(Name = "Risk Level")]
    public RiskLevel RiskRating { get; set; }
    
    //Relations
    public List<Inspection> Inspections { get; set; } = new();
}