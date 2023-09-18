using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Model.ResponseModel
{
    public class StatusResponse<T> where T : class
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<T>? Results { get; set; } = Enumerable.Empty<T>();
        public T? Result { get; set; }
    }
}
