namespace Strainth.BizService.Repositories.Setup;
public interface IExercisesRepository
{
    IQueryable<ExerciseDto> GetMany(FilterExercisesBy filterBy = FilterExercisesBy.None, string filterValue = "");
    Task<ExerciseDto> GetSingle(int id);
}