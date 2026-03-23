namespace FoodSafety.Library;

public enum Outcomes
{
    Pass,
    Fail,
}

public class Inspection
{
    public int Id { get; set; }
    public DateOnly InspectionDate { get; set; }
    public int Score { get; set; }
    public Outcomes OutCome { get; set; }
    public string Notes { get; set; } = "";
    
    // PK's
    public int PremisesId { get; set; }
    public Premises? Premises { get; set; }
    
    //Relations
    public List<FollowUp> FollowUps { get; set; } = new();
}