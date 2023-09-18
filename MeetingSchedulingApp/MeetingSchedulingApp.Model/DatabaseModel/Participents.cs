using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingSchedulingApp.Model.DatabaseModel
{
    [Table(name: "Participants", Schema = "public")]
    public class Participants
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Meeting_Id { get; set; }

        [Required]
        public int User_Id { get; set; }

        public string? Status { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("Meeting_Id")]
        public virtual Meetings? Meeting { get; set; }

        [ForeignKey("User_Id")]
        public virtual Users? User { get; set; }
    }
}
