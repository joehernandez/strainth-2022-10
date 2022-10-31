using Strainth.BizService.DTOs.Setup;
using Strainth.BizService.Repositories.Setup;

namespace Strainth.Api.Controllers;

public class ExercisesController : StrainthApiBaseController
{
    private readonly ExercisesRepository _exerciseRepository;

    public ExercisesController(ExercisesRepository exerciseRepository)
    {
        this._exerciseRepository = exerciseRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<ExerciseDto>>> GetExercises()
    {
        var exerciseDtos = await _exerciseRepository.GetMany().ToListAsync();
        return exerciseDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseDto>> GetExercise(int id)
    {
        if (id <= 0) return BadRequest();

        var exerciseDto = await _exerciseRepository.GetSingle(id);
        if (exerciseDto == null) return NotFound();

        return Ok(exerciseDto);
    }
}