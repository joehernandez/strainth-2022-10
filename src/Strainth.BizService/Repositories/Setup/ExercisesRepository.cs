using Microsoft.Extensions.Logging;

namespace Strainth.BizService.Repositories.Setup;

public enum FilterExercisesBy
{
    None,
    Category
}

public class ExercisesRepository : IExercisesRepository
{
    private readonly StrainthContext _strainthContext;
    private readonly ILogger<ExercisesRepository> _logger;

    public ExercisesRepository(StrainthContext strainthContext, ILogger<ExercisesRepository> logger)
    {
        _logger = logger;
        _strainthContext = strainthContext;
    }

    public async Task<ExerciseDto> GetSingle(int id)
    {
        var exercise = await _strainthContext.Exercises
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);

        return StrainthMapping.Mapper.Map<ExerciseDto>(exercise);
    }

    public IQueryable<ExerciseDto> GetMany(FilterExercisesBy filterBy = FilterExercisesBy.None, string filterValue = "")
    {
        var exercisesQuery = _strainthContext.Exercises
            .Include(c => c.Category);

        var filteredQuery = filterBy != FilterExercisesBy.None && !string.IsNullOrEmpty(filterValue)
            ? exercisesQuery.Where(c => c.Category.Name == filterValue)
            : null;

        var orderedQuery = (filteredQuery ?? exercisesQuery)
            .OrderBy(e => e.Category.Name)
            .ThenBy(e => e.Name);

        return orderedQuery.ProjectTo<ExerciseDto>(StrainthMapping.Config);
    }
}
