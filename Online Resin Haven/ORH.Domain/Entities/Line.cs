namespace ORH.Domain.Entities
{
    public class Line : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<Character> Characters { get; set; } = [];
    }
}
