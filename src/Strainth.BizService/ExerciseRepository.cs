using Strainth.BizService.DTOs.Setup;
using Strainth.DataService.Data;

namespace Strainth.BizService;

public class ExerciseRepository
{
    private readonly StrainthContext _strainthContext;

    public ExerciseRepository(StrainthContext strainthContext)
    {
        _strainthContext = strainthContext;
    }

    public IQueryable<ExerciseDto> GetAll()
    {
        var fakeReturn = new List<ExerciseDto>().AsQueryable();
        return fakeReturn;
    }
}
