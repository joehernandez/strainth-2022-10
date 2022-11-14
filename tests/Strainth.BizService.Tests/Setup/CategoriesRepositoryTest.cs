using Microsoft.Extensions.Logging;
using Moq;
using Strainth.BizService.DTOs.Setup;

namespace Strainth.BizService.Tests.Setup
{
    public class CategoriesRepositoryTest
    {
        private readonly StrainthContext _strainthContext;
        private readonly CategoriesRepository _categoriesRepository;
        private readonly Mock<ILogger<CategoriesRepository>> _loggerMock;

        public CategoriesRepositoryTest()
        {
            _loggerMock = new Mock<ILogger<CategoriesRepository>>();
            var options = SqliteInMemory.CreateOptions<StrainthContext>();
            _strainthContext = new StrainthContext(options);
            _categoriesRepository = new CategoriesRepository(_strainthContext, _loggerMock.Object);
        }

        [Fact]
        public async Task GetMany_Categories_Should_Get_All_Seeded_When_Not_Filtered()
        {
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
            var categoryDtos = await _categoriesRepository.GetMany().ToListAsync();

            categoryDtos.Count.Should().BeGreaterThan(10);
        }

        [Fact]
        public async Task GetMany_Categories_Should_OrderBy_Name()
        {
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
            var categoryDtos = await _categoriesRepository.GetMany().ToListAsync();
            var firstCategory = categoryDtos[0];

            firstCategory.Name.Should().Be("Abs");
        }

        [Fact]
        public async Task GetMany_Categories_Should_FilterBy_Name_When_Provided()
        {
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
            var categoryDtos = await _categoriesRepository.GetMany(FilterCategoryBy.Name, "Abs").ToListAsync();

            categoryDtos.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetMany_Categories_Should_Not_FilterBy_When_FilterValue_NotProvided()
        {
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
            var categoryDtos = await _categoriesRepository.GetMany(FilterCategoryBy.Name, "").ToListAsync();

            using var assertionScope = new AssertionScope();
            categoryDtos.Count.Should().BeGreaterThan(10);
        }

        [Fact]
        public async Task GetSingle_Categories_Should_Not_BeNull_When_Category_Id_IsValid()
        {
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
            var categoryDto = await _categoriesRepository.GetSingle(1);

            categoryDto.Name.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSingle_Categories_Should_BeNull_When_Category_Id_Not_IsValid()
        {
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
            var categoryDto = await _categoriesRepository.GetSingle(1000000);

            categoryDto.Should().BeNull();
        }

        [Fact]
        public async Task Add_Category_Returns_New_CategoryDto_When_Category_IsAdded()
        {
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
            var categoryDto = new CategoryDto
            {
                Name = "Test Category"
            };
            var totalCategories = await _categoriesRepository.GetMany().CountAsync();
            var newCategory = await _categoriesRepository.Add(categoryDto);
            var newTotalCategories = await _categoriesRepository.GetMany().CountAsync();

            newCategory.Id.Should().BeGreaterThan(0);
            newTotalCategories.Should().Be(totalCategories + 1);
        }

        [Fact]
        public async Task Add_Category_Does_Not_Create_New_Category_When_Duplicate_Category_IsAdded()
        {
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
            var categoryDto = new CategoryDto
            {
                Name = "Abs"
            };
            var totalCategories = await _categoriesRepository.GetMany().CountAsync();
            await _categoriesRepository.Add(categoryDto);
            var newTotalCategories = await _categoriesRepository.GetMany().CountAsync();

            newTotalCategories.Should().Be(totalCategories);
        }
    }
}
