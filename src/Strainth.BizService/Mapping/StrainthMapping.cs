using AutoMapper;

namespace Strainth.BizService.Mapping;

public static class StrainthMapping
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<StrainthProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper = Lazy.Value;
}

/*
 * then in code: var destination = StrainthMapping.Mapper.Map<Destination>(yourSourceInstance);
 * Based off this SO answer: https://stackoverflow.com/questions/26458731/how-to-configure-auto-mapper-in-class-library-project
 * by Marko
 */