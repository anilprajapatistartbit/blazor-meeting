using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingSchedulingApp.Model.DatabaseModel
{
    [Table(name: "Users", Schema = "public")]
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Profile { get; set; }

        public bool? IsActive { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
