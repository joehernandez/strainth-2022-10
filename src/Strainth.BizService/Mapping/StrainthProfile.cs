using AutoMapper;
using Strainth.BizService.DTOs.Programming;
using Strainth.BizService.DTOs.Setup;
using Strainth.DataService.Entities.Programming;
using Strainth.DataService.Entities.Setup;

namespace Strainth.BizService.Mapping;

public class StrainthProfile : Profile
{
    public StrainthProfile()
    {
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<ProgramSplitDto, ProgramSplit>();
    }
}
