using MeetingSchedulingApp.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Application.Interfaces
{
    public interface ILoginService
    {
        Task<Login> Create(Login login);
        Task<Login> GetByEmail(string  id);
        Task<Login> GetById_Email(string eid, int id);
        Task<Login> Update(Login login);
    }
}
