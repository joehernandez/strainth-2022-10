using Strainth.BizService.DTOs.Setup;
using Strainth.BizService.Repositories.Setup;

namespace Strainth.Api.Controllers
{
    public class CategoriesController : StrainthApiBaseController
    {
        private readonly CategoriesRepository _categoriesRepository;

        public CategoriesController(CategoriesRepository categoriesRepository)
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
