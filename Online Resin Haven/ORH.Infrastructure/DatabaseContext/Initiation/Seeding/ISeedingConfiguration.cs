using System.Text.Json;

namespace ORH.Infrastructure.DatabaseContext.Initiation.Seeding
{
    public interface ISeedingConfiguration
    {
        Type Type { get; }
        Task PopulateAsync(JsonSerializerOptions jsonSerializerOptions);
    }
}
