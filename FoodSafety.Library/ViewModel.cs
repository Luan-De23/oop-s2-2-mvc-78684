namespace FoodSafety.Library;

public class ViewModel
{
    public int TotalPremises { get; set; }
    
    public int TotalInspections { get; set; }
    public int TotalFail { get; set; }
    public List<Inspection> AllInspections { get; set; } = new();

    public int OverdueFollowUps { get; set; }
}