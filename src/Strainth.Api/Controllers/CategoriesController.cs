using Strainth.BizService.DTOs.Setup;
using Strainth.BizService.Repositories.Setup;

namespace Strainth.Api.Controllers
{
    public class CategoriesController : StrainthApiBaseController
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<List<CategoryDto>> GetCategories()
        {
            var categoryDtos = await _categoriesRepository.GetMany().ToListAsync();
            return categoryDtos;
        }
    }
}
