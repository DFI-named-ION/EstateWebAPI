namespace EstateWebAPI.Models.DTO
{
    public class OrderItemDto
    {
        public long OrderId { get; set; }
        public long EstateId { get; set; }
        public long Count { get; set; }
    }
}
