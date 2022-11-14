using AutoMapper;
using Strainth.BizService.DTOs.Programming;
using Strainth.DataService.Entities.Programming;
using Strainth.DataService.Entities.Setup;

namespace Strainth.BizService.Mapping;

public class StrainthProfile : Profile
{
    public StrainthProfile()
    {
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<ExerciseDto, Exercise>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();

        CreateMap<ProgramExercise, ProgramExerciseDto>();
        CreateMap<ProgramExerciseDto, ProgramExercise>();

        CreateMap<ProgramDetail, ProgramDetailDto>();
        CreateMap<ProgramDetailDto, ProgramDetail>();

        CreateMap<ProgramSplitDto, ProgramSplit>();
        CreateMap<ProgramSplit, ProgramSplitDto>();
    }
}
