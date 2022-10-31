namespace Strainth.BizService.Repositories;

public class ProgrammingRepository
{
    private readonly StrainthContext _strainthContext;

    public ProgrammingRepository(StrainthContext strainthContext)
    {
        _strainthContext = strainthContext;
    }


}
