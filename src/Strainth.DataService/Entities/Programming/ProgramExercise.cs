using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Strainth.DataService.Entities.Setup;

namespace Strainth.DataService.Entities.Programming;

[Table("ProgramExcercises")]
public class ProgramExercise
{
    public int Id { get; set; }
    public int RepsRangeLower { get; set; }
    public int RepsRangeUpper { get; set; }
    public int SetsRangeLower { get; set; }
    public int SetsRangeUpper { get; set; }
    public int RepsThreshold { get; set; }
    [Precision(6,2)]
    public decimal WeightIncrement { get; set; }

    // Navigation
    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; }
    public int ProgramDetailId { get; set; }
    public ProgramDetail ProgramDetail { get; set; }
}