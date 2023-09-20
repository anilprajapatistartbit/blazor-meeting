using MeetingSchedulingApp.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Application.Interfaces
{
    public interface IParticipantService
    {
        Task<Participants> Create(Participants participants);
        Task<Participants> Update(Participants participants);
        Task<Participants> Delete(int id);
        Task<Participants> Get(int id);
        Task<IEnumerable<Participants>> GetAll(int id);
        Task<IEnumerable<Participants>> GetAllByMeetingId(int id);

    }
}
