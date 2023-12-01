namespace EstateWebAPI.Models.DTO
{
    public class EstateCommentDto
    {
        public String Text { get; set; }
        public long UserId { get; set; }
        public long EstateId { get; set; }
    }
}
