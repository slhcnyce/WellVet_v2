using System;
using System.ComponentModel.DataAnnotations;

namespace WellVet_v2.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
