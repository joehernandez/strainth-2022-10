using Strainth.BizService.DTOs.Setup;
using Strainth.BizService.Repositories;

namespace Strainth.Api.Controllers;

public class ExerciseController : StrainthApiBaseController
{
    private readonly ExercisesRepository _exerciseRepository;

    public ExerciseController(ExercisesRepository exerciseRepository)
    {
        this._exerciseRepository = exerciseRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<ExerciseDto>>> GetExercises()
    {
        var exerciseDtos = await _exerciseRepository.GetMany().ToListAsync();
        return exerciseDtos;
    }
}