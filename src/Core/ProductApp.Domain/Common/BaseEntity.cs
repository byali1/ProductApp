namespace ProductApp.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
