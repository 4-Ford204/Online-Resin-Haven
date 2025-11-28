using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Character;

namespace ORH.Application.UseCase.Character.Queries
{
    public record GetCharactersQuery : IQuery<Result<List<GetCharactersResponse>>>;

    public class GetCharactersHandler(IGetCharacters service) : IQueryHandler<GetCharactersQuery, Result<List<GetCharactersResponse>>>
    {
        public async Task<Result<List<GetCharactersResponse>>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var characters = await service.ExecuteGetCharactersAsync();
                return Result.Success(characters);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }

    public class GetCharactersResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int ProductNumber { get; set; }
    }
}
