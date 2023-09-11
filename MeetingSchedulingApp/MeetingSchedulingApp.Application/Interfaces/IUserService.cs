using MeetingSchedulingApp.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<Users> Create(Users users);
        Task<Users> Update(Users users);
        Task<Users> Delete(int id);
        Task<Users> Get(int id);
        Task<Users> GetByEmailId(string id);
        Task<IEnumerable<Users>> GetAll();
    }
}
