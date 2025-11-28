using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Line;

namespace ORH.Application.UseCase.Line.Queries
{
    public record GetLinesQuery : IQuery<Result<List<GetLinesResponse>>>;

    public class GetLinesHandler(IGetLines service) : IQueryHandler<GetLinesQuery, Result<List<GetLinesResponse>>>
    {
        public async Task<Result<List<GetLinesResponse>>> Handle(GetLinesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lines = await service.ExecuteGetLinesAsync();
                return Result.Success(lines);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }

    public class GetLinesResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public IEnumerable<string>? Characters { get; set; }
    }
}
