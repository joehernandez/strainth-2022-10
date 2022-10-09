using System.ComponentModel.DataAnnotations.Schema;

namespace Strainth.DataService.Entities.Setup;

[Table("Categories")]
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation properties
    public ICollection<Exercise> Exercises { get; set; }
}