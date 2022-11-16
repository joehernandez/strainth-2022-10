namespace Strainth.BizService.Tests.Setup
{
    public class CategoriesRepositoryTest
    {
        private readonly StrainthContext _strainthContext;
        private readonly CategoriesRepository _categoriesRepository;
        private readonly Mock<AbstractTestLogger<CategoriesRepository>> _loggerMock;

        public CategoriesRepositoryTest()
        {
            _loggerMock = new Mock<AbstractTestLogger<CategoriesRepository>>();
            var options = SqliteInMemory.CreateOptions<StrainthContext>();
            _strainthContext = new StrainthContext(options);
            _categoriesRepository = new CategoriesRepository(_strainthContext, _loggerMock.Object);
            _strainthContext.Database.EnsureCreated();

            DevTestData.SeedTestData(_strainthContext);
        }

        [Fact]
        public async Task GetMany_Categories_Should_Get_All_Seeded_When_Not_Filtered()
        {
            var categoryDtos = await _categoriesRepository.GetMany().ToListAsync();

            categoryDtos.Count.Should().BeGreaterThan(10);
        }

        [Fact]
        public async Task GetMany_Categories_Should_OrderBy_Name()
        {
            var categoryDtos = await _categoriesRepository.GetMany().ToListAsync();
            var firstCategory = categoryDtos[0];

            firstCategory.Name.Should().Be("Abs");
        }

        [Fact]
        public async Task GetMany_Categories_Should_FilterBy_Name_When_Provided()
        {
            var categoryDtos = await _categoriesRepository.GetMany(FilterCategoryBy.Name, "Abs").ToListAsync();

            categoryDtos.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetMany_Categories_Should_Not_FilterBy_When_FilterValue_NotProvided()
        {
            var categoryDtos = await _categoriesRepository.GetMany(FilterCategoryBy.Name, "").ToListAsync();

            using var assertionScope = new AssertionScope();
            categoryDtos.Count.Should().BeGreaterThan(10);
        }

        [Fact]
        public async Task GetSingle_Categories_Should_Not_BeNull_When_Category_Id_IsValid()
        {
            var categoryDto = await _categoriesRepository.GetSingle(1);

            categoryDto.Name.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSingle_Categories_Should_BeNull_When_Category_Id_Not_IsValid()
        {
            var categoryDto = await _categoriesRepository.GetSingle(1000000);

            categoryDto.Should().BeNull();
        }

        [Fact]
        public async Task Add_Creates_New_Category_When_NonDuplicate_IsAdded()
        {
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
        public async Task Add_Does_Not_Create_New_Category_When_Duplicate_IsAdded()
        {
            var categoryDto = new CategoryDto
            {
                Name = "Abs"
            };
            var totalCategories = await _categoriesRepository.GetMany().CountAsync();
            await _categoriesRepository.Add(categoryDto);
            var newTotalCategories = await _categoriesRepository.GetMany().CountAsync();

            newTotalCategories.Should().Be(totalCategories);
        }

        // TODO: Uncomment when validation is implemented
        // [Fact]
        // public async Task Add_Logs_Error_When_CategoryDto_Missing_Required_Data()
        // {
        //     var categoryDto = new CategoryDto
        //     {
        //         Name = "",
        //     };
        //     await _categoriesRepository.Add(categoryDto);

        //     const string partialErrorMessage = "Error adding category with CategoryDto";
        //     _loggerMock.Verify(x =>
        //         x.Log(LogLevel.Error, It.IsAny<Exception>(), It.Is<string>(s => s.StartsWith(partialErrorMessage))), Times.Once);
        // }
    }
}
