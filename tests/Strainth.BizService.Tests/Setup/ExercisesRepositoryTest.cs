namespace Strainth.BizService.Tests.Setup;

public class ExercisesRepositoryTest
{
    [Fact]
    public async Task GetMany_Exercises_Should_Get_All_Seeded_When_Not_Filtered()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercises = await exerciseRepository.GetMany().ToListAsync();

        exercises.Count.Should().Be(27);
    }

    [Fact]
    public async Task GetMany_Exercises_Should_Project_CategoryName()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercises = await exerciseRepository.GetMany().ToListAsync();

        exercises.First().CategoryName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetMany_Exercises_Should_OrderBy_CategoryThenExercise()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercises = await exerciseRepository.GetMany().ToListAsync();
        var firstExercise = exercises.First();

        using var assertionScope = new AssertionScope();
        firstExercise.CategoryName.Should().Be("Abs");
        firstExercise.Name.Should().Be("Ab Wheel");
    }

    [Fact]
    public async Task GetMany_Exercises_Should_FilterBy_Category_When_Provided()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercises = await exerciseRepository.GetMany(FilterExercisesBy.Category, "Abs").ToListAsync();

        using var assertionScope = new AssertionScope();
        exercises.Count.Should().Be(2);
        exercises.Last().Name.Should().Be("Bicycle Crunch");
    }

    [Fact]
    public async Task GetSingle_Exercise_Should_Not_Be_Null_For_Valid_Id()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercise = await exerciseRepository.GetSingle(1);

        exercise.Should().NotBeNull();
    }

    [Fact]
    public async Task GetSingle_Exercise_Should_Be_Null_For_Invalid_Id()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercise = await exerciseRepository.GetSingle(1000000);

        exercise.Should().BeNull();
    }
}