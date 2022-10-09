namespace Strainth.DataService.Entities.Setup;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation properties
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}