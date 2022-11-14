namespace Strainth.BizService.Repositories.Setup;

public interface ICategoriesRepository
{
    IQueryable<CategoryDto> GetMany(FilterCategoryBy filterBy = FilterCategoryBy.None, string filterValue = "");
    Task<CategoryDto> GetSingle(int id);
    Task<CategoryDto> Add(CategoryDto categoryDto);
}