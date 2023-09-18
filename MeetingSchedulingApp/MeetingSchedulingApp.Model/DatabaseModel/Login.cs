using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingSchedulingApp.Model.DatabaseModel
{
    [Table(name: "Login", Schema = "public")]
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int User_Id { get; set; }

        [Required]
        public string HashPassword { get; set; }

        [Required]
        public string SaltPassword { get; set; }

        public bool? IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        [ForeignKey("User_Id")]
        public virtual Users? User { get; set; }
    }
}
