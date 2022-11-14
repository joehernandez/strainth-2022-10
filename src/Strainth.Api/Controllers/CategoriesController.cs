using Strainth.BizService.DTOs.Setup;
using Strainth.BizService.Repositories.Setup;

namespace Strainth.Api.Controllers
{
    public class CategoriesController : StrainthApiBaseController
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoriesRepository categoriesRepository, ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categoryDtos = await _categoriesRepository.GetMany().ToListAsync();
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var categoryIdParam = new KeyValuePair<string, int>("categoryId", id);
            if (id <= 0) return HandleBadRequest(_logger, new object[] { categoryIdParam });

            var categoryDtos = await _categoriesRepository.GetSingle(id);
            if (categoryDtos == null)
            {
                return HandleNotFoundRequest(_logger, nameof(CategoryDto), new object[] { categoryIdParam });
            }
            return Ok(categoryDtos);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryDto categoryDto)
        {
            var newCategory = await _categoriesRepository.Add(categoryDto);
            if (newCategory == null)
            {
                var categoryDtoParam = new KeyValuePair<string, CategoryDto>("categoryDto", categoryDto);
                HandleBadRequest(_logger, new object[] { categoryDtoParam });
            }

            return CreatedAtRoute(nameof(GetCategory), new { id = newCategory.Id }, newCategory);
        }
    }
}
