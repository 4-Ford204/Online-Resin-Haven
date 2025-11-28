using ORH.Application.UseCase.Studio.Queries;

namespace ORH.Application.Interface.Studio
{
    public interface IGetStudios
    {
        Task<List<GetStudiosResponse>> ExecuteGetStudiosAsync();
    }
}
