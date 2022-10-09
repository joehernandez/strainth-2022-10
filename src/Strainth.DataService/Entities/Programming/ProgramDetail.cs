using System.ComponentModel.DataAnnotations.Schema;

namespace Strainth.DataService.Entities.Programming;

[Table("ProgramDetails")]
public class ProgramDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DayNumber { get; set; }

    // Navigation
    public int ProgramSplitId { get; set; }
    public ProgramSplit ProgramSplit { get; set; }
    public ICollection<ProgramExercise> ProgramExercises { get; set; }
    // public List<ProgramExecution> ProgramExecution { get; set; }
}