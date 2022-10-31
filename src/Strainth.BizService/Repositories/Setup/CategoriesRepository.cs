namespace Strainth.BizService.Repositories.Setup
{
    public enum FilterCategoryBy
    {
        None,
        Name
    }
    public class CategoriesRepository
    {
        private readonly StrainthContext _strainthContext;

        public CategoriesRepository(StrainthContext strainthContext)
        {
            _strainthContext = strainthContext;
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

            var projectedQuery = orderedByQuery.ProjectTo<CategoryDto>(StrainthMapping.Config);
            return projectedQuery;
        }
    }
}
