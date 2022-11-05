using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strainth.BizService.Repositories.Setup;

public interface ICategoriesRepository
{
    IQueryable<CategoryDto> GetMany(FilterCategoryBy filterBy = FilterCategoryBy.None, string filterValue = "");
}