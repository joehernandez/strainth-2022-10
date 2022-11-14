namespace Strainth.BizService.Tests.Setup
{
    public class CategoriesRepositoryTest
    {
        [Fact]
        public async Task GetMany_Categories_Should_Get_All_Seeded_When_Not_Filtered()
        {
            var options = SqliteInMemory.CreateOptions<StrainthContext>();
            using var context = new StrainthContext(options);
            context.Database.EnsureCreated();

            DevTestData.SeedTestData(context);
            var categoryRepository = new CategoriesRepository(context);
            var categoryDtos = await categoryRepository.GetMany().ToListAsync();

            categoryDtos.Count.Should().BeGreaterThan(10);
        }

        [Fact]
        public async Task GetMany_Categories_Should_OrderBy_Name()
        {
            var options = SqliteInMemory.CreateOptions<StrainthContext>();
            using var context = new StrainthContext(options);
            context.Database.EnsureCreated();

            DevTestData.SeedTestData(context);
            var categoryRepository = new CategoriesRepository(context);
            var categoryDtos = await categoryRepository.GetMany().ToListAsync();
            var firstCategory = categoryDtos.First();

            firstCategory.Name.Should().Be("Abs");
        }

        [Fact]
        public async Task GetMany_Categories_Should_FilterBy_Name_When_Provided()
        {
            var options = SqliteInMemory.CreateOptions<StrainthContext>();
            using var context = new StrainthContext(options);
            context.Database.EnsureCreated();

            DevTestData.SeedTestData(context);
            var categoryRepository = new CategoriesRepository(context);
            var categoryDtos = await categoryRepository.GetMany(FilterCategoryBy.Name, "Abs").ToListAsync();

            categoryDtos.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetMany_Categories_Should_Not_FilterBy_When_FilterValue_NotProvided()
        {
            var options = SqliteInMemory.CreateOptions<StrainthContext>();
            using var context = new StrainthContext(options);
            context.Database.EnsureCreated();

            DevTestData.SeedTestData(context);
            var categoryRepository = new CategoriesRepository(context);
            var categoryDtos = await categoryRepository.GetMany(FilterCategoryBy.Name, "").ToListAsync();

            using var assertionScope = new AssertionScope();
            categoryDtos.Count.Should().BeGreaterThan(10);
        }
    }
}
