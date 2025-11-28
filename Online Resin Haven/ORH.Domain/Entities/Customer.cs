using ORH.Domain.Constant.Enums;

namespace ORH.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Gender Gender { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
