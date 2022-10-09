﻿namespace Strainth.DataService.Entities.Programming;
public class ProgramSplit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public ICollection<ProgramDetail> ProgramDetails { get; set; }
}