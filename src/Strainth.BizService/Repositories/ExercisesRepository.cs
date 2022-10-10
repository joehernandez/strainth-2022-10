namespace Strainth.BizService.Repositories;

public enum FilterExercisesBy
{
    None,
    Category
}

public class ExercisesRepository
{
    private readonly StrainthContext _strainthContext;

    public ExercisesRepository(StrainthContext strainthContext)
    {
        _strainthContext = strainthContext;
    }

    public IQueryable<ExerciseDto> GetMany(FilterExercisesBy filterBy = FilterExercisesBy.None, string filterValue = "")
    {
        var exercisesQuery = _strainthContext.Exercises
            .AsNoTracking()
            .Include(c => c.Category);

        var filteredQuery = filterBy != FilterExercisesBy.None
            ? exercisesQuery.Where(c => c.Category.Name == filterValue)
            : null;

        var orderedQuery = (filteredQuery is null ? exercisesQuery : filteredQuery)
            .OrderBy(e => e.Category.Name)
            .ThenBy(e => e.Name);

        var projectedQuery = orderedQuery.ProjectTo<ExerciseDto>(StrainthMapping.Config);
        return projectedQuery;
    }
}
