namespace ORH.Domain.Entities
{
    public class Studio : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<Product> Products { get; set; } = [];
    }
}
