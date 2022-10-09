using Microsoft.EntityFrameworkCore;
using Strainth.DataService.Data.SeedData;
using Strainth.DataService.Entities.Programming;
using Strainth.DataService.Entities.Setup;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Strainth.DataService.Data;

public class StrainthContext : DbContext
{
    public StrainthContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedPermanentData();
    }

    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ProgramSplit> ProgramSplits { get; set; }
}