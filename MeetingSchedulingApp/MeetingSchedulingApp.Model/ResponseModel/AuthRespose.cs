using MeetingSchedulingApp.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Model.ResponseModel
{
    public class AuthRespose
    {
        public bool isAuthenticated {get;set;}
        public Login? Login { get;set;}
    }
}
