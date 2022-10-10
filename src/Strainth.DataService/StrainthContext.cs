using Microsoft.EntityFrameworkCore;
using Strainth.DataService.Entities.Programming;
using Strainth.DataService.Entities.Setup;

namespace Strainth.DataService.Data;

public class StrainthContext : DbContext
{
    public StrainthContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ProgramSplit> ProgramSplits { get; set; }
}