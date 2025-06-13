using AutoMapper;
using CommonTestUtilities.IdEncryption;
using MyRecipeBook.Application.Services.AutoMapper;

namespace CommonTestUtilities.Mapper;

public static class MapperBuilder
{
    public static IMapper Build()
    {
        var idEncripter = IdEncripterBuilder.Build();

        return new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping(idEncripter));
        }).CreateMapper();
    }
}
