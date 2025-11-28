using ORH.Application.UseCase.Character.Queries;

namespace ORH.Application.Interface.Character
{
    public interface IGetCharacters
    {
        Task<List<GetCharactersResponse>> ExecuteGetCharactersAsync();
    }
}
