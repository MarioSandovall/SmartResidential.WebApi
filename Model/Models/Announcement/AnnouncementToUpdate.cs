using System.ComponentModel.DataAnnotations;

namespace Model.Models.Announcement
{
    public class AnnouncementToUpdate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        public int UserId { get; set; }

        public bool CanSendEmail { get; set; }
    }
}
