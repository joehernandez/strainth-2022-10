using Microsoft.Extensions.Logging;
using Strainth.DataService.Entities.Setup;

namespace Strainth.BizService.Repositories.Setup
{
    public enum FilterCategoryBy
    {
        None,
        Name
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly StrainthContext _strainthContext;
        private readonly ILogger<CategoriesRepository> _logger;

        public CategoriesRepository(StrainthContext strainthContext, ILogger<CategoriesRepository> logger)
        {
            _logger = logger;
            _strainthContext = strainthContext;
        }

        public async Task<CategoryDto> GetSingle(int id)
        {
            var category = await _strainthContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            return StrainthMapping.Mapper.Map<CategoryDto>(category);
        }

        public IQueryable<CategoryDto> GetMany(FilterCategoryBy filterBy = FilterCategoryBy.None, string filterValue = "")
        {
            var query = _strainthContext.Categories.AsQueryable();

            var filteredByQuery = (filterBy == FilterCategoryBy.None || string.IsNullOrEmpty(filterValue))
                ? null
                : query.Where(c => c.Name == filterValue);

            var orderedByQuery = filteredByQuery == null
                ? query.OrderBy(c => c.Name)
                : filteredByQuery.OrderBy(c => c.Name);

            return orderedByQuery.ProjectTo<CategoryDto>(StrainthMapping.Config);
        }

        public async Task<CategoryDto> Add(CategoryDto categoryDto)
        {
            var existingCategory = await _strainthContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryDto.Name);
            if (existingCategory != null)
            {
                return categoryDto;
            }

            try
            {
                var category = StrainthMapping.Mapper.Map<Category>(categoryDto);
                _strainthContext.Categories.Add(category);
                await _strainthContext.SaveChangesAsync();
                return StrainthMapping.Mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding category with CategoryDto: {categoryDto}", categoryDto);
            }

            return null;
        }
    }
}
