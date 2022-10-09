namespace Strainth.BizService.DTOs.Programming;

public class ProgramSplitDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public ICollection<ProgramDetailDto> ProgramDetailDtos { get; set; }
}
