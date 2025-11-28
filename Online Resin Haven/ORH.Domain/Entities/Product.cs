namespace ORH.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int? CharacterId { get; set; }
        public int? StudioId { get; set; }
        public required string Name { get; set; }
        public int Quantity { get; set; }
        public required float Price { get; set; }

        public Character? Character { get; set; }
        public Studio? Studio { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = [];
    }
}
