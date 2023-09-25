using MeetingSchedulingApp.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Model.ResponseModel
{
    public class MeetingGroupByDate
    {
        public DateTime scheduledDate { get; set; }
        public IEnumerable<Meetings> Meetings { get; set; }
    }
}
