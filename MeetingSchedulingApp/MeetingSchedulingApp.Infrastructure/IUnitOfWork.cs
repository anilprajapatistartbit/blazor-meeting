using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingSchedulingApp.Model.DatabaseModel;

namespace MeetingSchedulingApp.Infrastructure
{
    public interface IUnitOfWork
    {
        IGenericRepository<Login> Logins { get; }
        IGenericRepository<Users> Users { get; }
        IGenericRepository<Meetings> Meetings { get; }
        IGenericRepository<Participants> Participants { get; }
        Task<bool> CompleteAsync();
        void Dispose();
    }
}
