using MeetingSchedulingApp.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Application.Interfaces
{
    public interface IMeetingService
    {
        Task<Meetings> Create(Meetings meetings);
        Task<Meetings> Update(Meetings meetings);
        Task<Meetings> Delete(int id);
        Task<Meetings> Get(int id);
        Task<IEnumerable<Meetings>> GetAll();
    }
}
