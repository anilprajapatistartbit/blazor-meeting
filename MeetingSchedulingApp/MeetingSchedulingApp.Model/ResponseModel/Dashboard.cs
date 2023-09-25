using MeetingSchedulingApp.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Model.ResponseModel
{
    public class Dashboard
    {
        public IEnumerable<Meetings> Today { get; set; }
        public IEnumerable<MeetingGroupByDate> Upcoming { get; set; }
    }
}
