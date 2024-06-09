using System.ComponentModel.DataAnnotations.Schema;

namespace WellVet_v2.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public string Content { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
    }
}
