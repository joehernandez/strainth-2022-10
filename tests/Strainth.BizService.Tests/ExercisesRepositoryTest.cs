using Strainth.BizService.Repositories;

namespace Strainth.BizService.Tests;

public class ExercisesRepositoryTest
{
    [Fact]
    public void GetMany_Should_Get_All_Seeded_When_Not_Filtered()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercises = exerciseRepository.GetMany().ToList();

        exercises.Count.Should().Be(27);
    }

    [Fact]
    public void GetMany_Should_Project_CategoryName()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercises = exerciseRepository.GetMany().ToList();

        exercises.First().CategoryName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetMany_Should_OrderBy_CategoryThenExercise()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercises = exerciseRepository.GetMany().ToList();
        var firstExercise = exercises.First();

        using var assertionScope = new AssertionScope();
        firstExercise.CategoryName.Should().Be("Abs");
        firstExercise.Name.Should().Be("Ab Wheel");
    }

    [Fact]
    public void GetMany_Should_FilterBy_Category_When_Provided()
    {
        var options = SqliteInMemory.CreateOptions<StrainthContext>();
        using var context = new StrainthContext(options);
        context.Database.EnsureCreated();

        DevTestData.SeedTestData(context);
        var exerciseRepository = new ExercisesRepository(context);
        var exercises = exerciseRepository.GetMany(FilterExercisesBy.Category, "Abs").ToList();

        using var assertionScope = new AssertionScope();
        exercises.Count.Should().Be(2);
        exercises.Last().Name.Should().Be("Bicycle Crunch");
    }
}