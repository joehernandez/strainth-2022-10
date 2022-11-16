namespace Strainth.BizService.Tests.Setup;

public class ExercisesRepositoryTest
{
    private readonly Mock<AbstractTestLogger<ExercisesRepository>> _loggerMock;
    private readonly StrainthContext _strainthContext;
    private readonly ExercisesRepository _exercisesRepository;

    public ExercisesRepositoryTest()
    {
        _loggerMock = new Mock<AbstractTestLogger<ExercisesRepository>>();
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        _strainthContext = new StrainthContext(options);
        _exercisesRepository = new ExercisesRepository(_strainthContext, _loggerMock.Object);
        _strainthContext.Database.EnsureCreated();
        DevTestData.SeedTestData(_strainthContext);
    }

    [Fact]
    public async Task GetMany_Exercises_Should_Get_All_Seeded_When_Not_Filtered()
    {
        var exercises = await _exercisesRepository.GetMany().ToListAsync();

        exercises.Count.Should().Be(27);
    }

    [Fact]
    public async Task GetMany_Exercises_Should_Project_CategoryName()
    {
        var exerciseDtos = await _exercisesRepository.GetMany().ToListAsync();

        exerciseDtos[0].CategoryName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetMany_Exercises_Should_OrderBy_CategoryThenExercise()
    {
        var exercises = await _exercisesRepository.GetMany().ToListAsync();
        var firstExercise = exercises[0];

        using var assertionScope = new AssertionScope();
        firstExercise.CategoryName.Should().Be("Abs");
        firstExercise.Name.Should().Be("Ab Wheel");
    }

    [Fact]
    public async Task GetMany_Exercises_Should_FilterBy_Category_When_Provided()
    {
        var exercises = await _exercisesRepository.GetMany(FilterExercisesBy.Category, "Abs").ToListAsync();

        using var assertionScope = new AssertionScope();
        exercises.Count.Should().Be(2);
        exercises.Last().Name.Should().Be("Bicycle Crunch");
    }

    [Fact]
    public async Task GetSingle_Exercise_Should_Not_Be_Null_For_Valid_Id()
    {
        var exercise = await _exercisesRepository.GetSingle(1);

        exercise.Should().NotBeNull();
    }

    [Fact]
    public async Task GetSingle_Exercise_Should_Be_Null_For_Invalid_Id()
    {
        var exercise = await _exercisesRepository.GetSingle(1000000);

        exercise.Should().BeNull();
    }

    [Fact]
    public async Task GetSingle_Exercise_Should_Project_CategoryName()
    {
        var exerciseDto = await _exercisesRepository.GetSingle(1);

        exerciseDto.CategoryName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Add_Creates_New_Exercise_When_NonDuplicate_IsAdded()
    {
        var exerciseDto = new ExerciseDto
        {
            Name = "Test Exercise",
            CategoryName = "Abs",
            CategoryId = 1
        };
        var totalExercises = await _exercisesRepository.GetMany().CountAsync();
        var newExercise = await _exercisesRepository.Add(exerciseDto);
        var newTotalExercises = await _exercisesRepository.GetMany().CountAsync();

        newExercise.Id.Should().BeGreaterThan(0);
        newTotalExercises.Should().Be(totalExercises + 1);
    }

    [Fact]
    public async Task Add_Does_Not_Create_New_Exercise_When_Duplicate_IsAdded()
    {
        var existingExercise = await _exercisesRepository.GetSingle(1);
        var totalExercises = await _exercisesRepository.GetMany().CountAsync();
        var newExercise = await _exercisesRepository.Add(
            new ExerciseDto
            {
                Name = existingExercise.Name,
                CategoryId = existingExercise.CategoryId,
                CategoryName = existingExercise.CategoryName
            });
        var newTotalExercises = await _exercisesRepository.GetMany().CountAsync();

        newExercise.Id.Should().Be(0);
        newTotalExercises.Should().Be(totalExercises);
    }

    [Fact]
    public async Task Add_Logs_Error_When_ExerciseDto_Missing_Required_Data()
    {
        var exerciseDto = new ExerciseDto
        {
            Name = "Test Exercise",
            CategoryName = "Abs"
        };
        await _exercisesRepository.Add(exerciseDto);

        const string partialErrorMessage = "Error adding exercise with ExerciseDto";
        _loggerMock.Verify(x => x.Log(LogLevel.Error, It.IsAny<Exception>(), It.Is<string>(s => s.StartsWith(partialErrorMessage))), Times.Once);
    }
}