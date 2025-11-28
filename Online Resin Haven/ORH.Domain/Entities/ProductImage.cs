namespace ORH.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public int? ProductId { get; set; }
        public required string URL { get; set; }

        public Product? Product { get; set; }
    }
}
