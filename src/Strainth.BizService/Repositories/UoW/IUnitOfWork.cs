using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strainth.BizService.Repositories.UoW;

public interface IUnitOfWork
{
    Task SaveAllAsync();
}