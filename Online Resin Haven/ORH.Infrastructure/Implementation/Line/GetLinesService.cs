using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Application.Interface.Line;
using ORH.Application.UseCase.Line.Queries;
using ORH.Domain.Repositories;

namespace ORH.Infrastructure.Implementation.Line
{
    [Service(ServiceLifetime.Scoped)]
    public class GetLinesService(IUnitOfWork context) : IGetLines
    {
        private readonly IUnitOfWork _context = context;

        public async Task<List<GetLinesResponse>> ExecuteGetLinesAsync()
        {
            var result = await _context.LineRepository.GetLinesQueryable()
                .GroupJoin(
                    _context.CharacterRepository.GetCharactersQueryable(),
                    line => line.Id,
                    character => character.LineId,
                    (line, characters) => new GetLinesResponse
                    {
                        Id = line.Id,
                        Name = line.Name,
                        Characters = characters.Select(c => c.Name)
                    }
                )
                .ToListAsync();

            return result;
        }
    }
}
