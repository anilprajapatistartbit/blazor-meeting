using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingSchedulingApp.Model.DatabaseModel
{
    [Table(name: "Meetings", Schema = "public")]
    public class Meetings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public int Host_Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Status { get; set; }

        [Required]
        public string Meeting_Type { get; set; }

        [Required]
        public string Location { get; set; }

        [ForeignKey("Host_Id")]
        public virtual Users? Host { get; set; }
    }
}