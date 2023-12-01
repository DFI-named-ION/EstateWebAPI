namespace EstateWebAPI.Models.DTO
{
    public class EstateDto
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long OwnerId { get; set; }
        public decimal Price { get; set; }
        public long FloorCount { get; set; }
        public long RoomCount { get; set; }
        public long CategoryId { get; set; }
    }
}
