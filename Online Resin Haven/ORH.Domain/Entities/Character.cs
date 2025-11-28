namespace ORH.Domain.Entities
{
    public class Character : BaseEntity
    {
        public int? LineId { get; set; }
        public required string Name { get; set; }

        public Line? Line { get; set; }
        public ICollection<Product> Products { get; set; } = [];
    }
}
