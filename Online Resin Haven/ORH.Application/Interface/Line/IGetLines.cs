using ORH.Application.UseCase.Line.Queries;

namespace ORH.Application.Interface.Line
{
    public interface IGetLines
    {
        Task<List<GetLinesResponse>> ExecuteGetLinesAsync();
    }
}
