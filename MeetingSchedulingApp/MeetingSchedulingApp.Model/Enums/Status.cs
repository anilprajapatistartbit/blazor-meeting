using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Model.Enums
{
    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }
    public enum Status
    {
        PENDING = 1,
        COMPLETED = 2,
        STARTED = 3,
        CANCELLED = 4,

    }

   public static class GetEnum
    {
        public static List<string>  StatusData()
        {
            List<string> Key = Enum.GetNames(typeof(Status))
                                .Cast<string>()
                                .ToList();
        return Key;
        }
      
    }
    

}
