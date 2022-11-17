using Strainth.BizService.DTOs.Setup;
using Strainth.BizService.Repositories.Setup;

namespace Strainth.Api.Controllers;

public class ExercisesController : StrainthApiBaseController
{
    private readonly IExercisesRepository _exerciseRepository;
    private readonly ILogger<ExercisesController> _logger;

    public ExercisesController(IExercisesRepository exerciseRepository, ILogger<ExercisesController> logger)
    {
        _logger = logger;
        _exerciseRepository = exerciseRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExerciseDto>>> GetExercises()
    {
        var exerciseDtos = await _exerciseRepository.GetMany().ToListAsync();
        return Ok(exerciseDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseDto>> GetExercise(int id)
    {
        var exerciseIdParam = new KeyValuePair<string, int>("exerciseId", id);
        // TODO: var userIdParam = new KeyValuePair<string, int>("userId", userId);
        if (id <= 0)
        {
            return HandleBadRequest(_logger, new object[] { exerciseIdParam });
        }

        var exerciseDto = await _exerciseRepository.GetSingle(id);
        if (exerciseDto == null)
        {
            return HandleNotFoundRequest(_logger, nameof(ExerciseDto), new object[] { exerciseIdParam });
        }

        return Ok(exerciseDto);
    }

    [HttpPost]
    public async Task<ActionResult> AddExercise(ExerciseDto exerciseDto)
    {
        var newExercise = await _exerciseRepository.Add(exerciseDto);
        if (newExercise.Id <= 0)
        {
            var exerciseDtoParam = new KeyValuePair<string, ExerciseDto>("exerciseDto", exerciseDto);
            return HandleBadRequest(_logger, new object[] { exerciseDtoParam });
        }

        return CreatedAtAction(nameof(GetExercise), new { id = newExercise.Id }, newExercise);
    }
}