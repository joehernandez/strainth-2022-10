using Microsoft.Extensions.Logging;
using Strainth.DataService.Entities.Setup;

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

    public async Task<ExerciseDto> Add(ExerciseDto exerciseDto)
    {
        var existingExercise = await _strainthContext.Exercises.FirstOrDefaultAsync(e => e.Name == exerciseDto.Name);
        if (existingExercise != null) return exerciseDto;

        try
        {
            var exercise = StrainthMapping.Mapper.Map<Exercise>(exerciseDto);
            _strainthContext.Exercises.Add(exercise);
            await _strainthContext.SaveChangesAsync();
            return StrainthMapping.Mapper.Map<ExerciseDto>(exercise);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding exercise with ExerciseDto: {exerciseDto}", exerciseDto);
            return null;
        }
    }
}
