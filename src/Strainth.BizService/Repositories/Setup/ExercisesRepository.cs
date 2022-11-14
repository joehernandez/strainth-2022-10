namespace Strainth.BizService.Repositories.Setup;

public enum FilterExercisesBy
{
    None,
    Category
}

public class ExercisesRepository : IExercisesRepository
{
    private readonly StrainthContext _strainthContext;

    public ExercisesRepository(StrainthContext strainthContext)
    {
        _strainthContext = strainthContext;
    }

    public async Task<ExerciseDto> GetSingle(int id)
    {
        var exercise = await _strainthContext.Exercises
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

        var projectedQuery = orderedQuery.ProjectTo<ExerciseDto>(StrainthMapping.Config);
        return projectedQuery;
    }
}
