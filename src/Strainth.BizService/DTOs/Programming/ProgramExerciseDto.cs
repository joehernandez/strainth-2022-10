using Strainth.BizService.DTOs.Setup;

namespace Strainth.BizService.DTOs.Programming;

public class ProgramExerciseDto
{
    public int Id { get; set; }
    public int RepsRangeLower { get; set; }
    public int RepsRangeUpper { get; set; }
    public int SetsRangeLower { get; set; }
    public int SetsRangeUpper { get; set; }
    public int RepsThreshold { get; set; }
    public decimal WeightIncrement { get; set; }
    public ExerciseDto ExerciseDto { get; set; }
}
