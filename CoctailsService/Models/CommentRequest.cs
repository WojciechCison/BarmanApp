namespace CoctailsService.Models
{
    public class CommentRequest
    {
        public int CoctailId { get; set; }

        public int UserId { get; set; }

        public string Comment { get; set; }
    }
}
