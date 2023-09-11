using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeetingSchedulingApp.Model.DatabaseModel;

namespace MeetingSchedulingApp.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        #endregion

        #region Fields
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Meetings> Meetings { get; set; }
        public virtual DbSet<Participants> Participants { get; set; }
        #endregion
    }
}
