namespace Directum.MessagerDAL.Models
{
    public class Contact
    {
        public int UserId { get; set; }

        public int ContactId { get; set; }

        public DateTime? LastUpdateTime { get; set; }
    }
}