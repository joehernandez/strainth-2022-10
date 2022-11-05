namespace Strainth.BizService.Repositories.Programming;

public class ProgrammingRepository
{
    private readonly StrainthContext _strainthContext;

    public ProgrammingRepository(StrainthContext strainthContext)
    {
        _strainthContext = strainthContext;
    }


}
