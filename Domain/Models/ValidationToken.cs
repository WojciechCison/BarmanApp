namespace Domain.Models
{
    public class ValidationToken
    {
        public int UserId { get; set; }

        public string Token { get; set; }

        public DateTime ExpireTime { get; set; }
    }
}
