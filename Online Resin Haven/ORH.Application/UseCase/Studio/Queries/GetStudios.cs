using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Studio;

namespace ORH.Application.UseCase.Studio.Queries
{
    public record GetStudiosQuery : IQuery<Result<List<GetStudiosResponse>>>;

    public class GetStudiosHandler(IGetStudios service) : IQueryHandler<GetStudiosQuery, Result<List<GetStudiosResponse>>>
    {
        public async Task<Result<List<GetStudiosResponse>>> Handle(GetStudiosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var studios = await service.ExecuteGetStudiosAsync();
                return Result.Success(studios);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }

    public class GetStudiosResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int ProductNumber { get; set; }
    }
}
