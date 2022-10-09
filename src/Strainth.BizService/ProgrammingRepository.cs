using Strainth.DataService.Data;

namespace Strainth.BizService;

public class ProgrammingRepository
{
    private readonly StrainthContext _strainthContext;

    public ProgrammingRepository(StrainthContext strainthContext)
    {
        this._strainthContext = strainthContext;
    }


}
