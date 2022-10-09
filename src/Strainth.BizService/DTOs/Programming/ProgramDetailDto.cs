namespace Strainth.BizService.DTOs.Programming;

public class ProgramDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DayNumber { get; set; }

    public ICollection<ProgramExerciseDto> ProgramExerciseDtos { get; set; }
}
