namespace Strainth.BizService.Tests.Setup;

public class ExercisesRepositoryTest
{
    private readonly Mock<ILogger<ExercisesRepository>> _loggerMock;
    private readonly StrainthContext _strainthContext;
    private readonly ExercisesRepository _exercisesRepository;

    public ExercisesRepositoryTest()
    {
        _loggerMock = new Mock<ILogger<ExercisesRepository>>();
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        _strainthContext = new StrainthContext(options);
        _exercisesRepository = new ExercisesRepository(_strainthContext, _loggerMock.Object);
    }

    [Fact]
    public async Task GetMany_Exercises_Should_Get_All_Seeded_When_Not_Filtered()
    {
        _strainthContext.Database.EnsureCreated();

        DevTestData.SeedTestData(_strainthContext);
        var exercises = await _exercisesRepository.GetMany().ToListAsync();

        exercises.Count.Should().Be(27);
    }

    [Fact]
    public async Task GetMany_Exercises_Should_Project_CategoryName()
    {
        _strainthContext.Database.EnsureCreated();

        DevTestData.SeedTestData(_strainthContext);
        var exerciseDtos = await _exercisesRepository.GetMany().ToListAsync();

        exerciseDtos[0].CategoryName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetMany_Exercises_Should_OrderBy_CategoryThenExercise()
    {
        _strainthContext.Database.EnsureCreated();

        DevTestData.SeedTestData(_strainthContext);
        var exercises = await _exercisesRepository.GetMany().ToListAsync();
        var firstExercise = exercises[0];

        using var assertionScope = new AssertionScope();
        firstExercise.CategoryName.Should().Be("Abs");
        firstExercise.Name.Should().Be("Ab Wheel");
    }

    [Fact]
    public async Task GetMany_Exercises_Should_FilterBy_Category_When_Provided()
    {
        _strainthContext.Database.EnsureCreated();

        DevTestData.SeedTestData(_strainthContext);
        var exercises = await _exercisesRepository.GetMany(FilterExercisesBy.Category, "Abs").ToListAsync();

        using var assertionScope = new AssertionScope();
        exercises.Count.Should().Be(2);
        exercises.Last().Name.Should().Be("Bicycle Crunch");
    }

    [Fact]
    public async Task GetSingle_Exercise_Should_Not_Be_Null_For_Valid_Id()
    {
        _strainthContext.Database.EnsureCreated();

        DevTestData.SeedTestData(_strainthContext);
        var exercise = await _exercisesRepository.GetSingle(1);

        exercise.Should().NotBeNull();
    }

    [Fact]
    public async Task GetSingle_Exercise_Should_Be_Null_For_Invalid_Id()
    {
        _strainthContext.Database.EnsureCreated();

        DevTestData.SeedTestData(_strainthContext);
        var exercise = await _exercisesRepository.GetSingle(1000000);

        exercise.Should().BeNull();
    }

    [Fact]
    public async Task GetSingle_Exercise_Should_Project_CategoryName()
    {
        _strainthContext.Database.EnsureCreated();

        DevTestData.SeedTestData(_strainthContext);
        var exerciseDto = await _exercisesRepository.GetSingle(1);

        exerciseDto.CategoryName.Should().NotBeNullOrEmpty();
    }
}