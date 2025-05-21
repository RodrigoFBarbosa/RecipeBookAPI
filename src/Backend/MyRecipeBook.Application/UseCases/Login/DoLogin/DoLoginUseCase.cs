using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IAccessTokenGenerator accessTokenGenerator,
        PasswordEncripter passwordEncripter)
    {
        _repository = repository;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encriptedPassword = _passwordEncripter.Encrypt(request.Password);

        var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier),
            }
        };
    }
}
