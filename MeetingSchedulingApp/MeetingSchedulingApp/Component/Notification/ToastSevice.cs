using MeetingSchedulingApp.Model.Enums;
using Microsoft.AspNetCore.Components;

namespace MeetingSchedulingApp.Component.Notification
{
    public class ToastService
    {
        public event Action<Notify>? Onchange;

        public async void Toast(string message, ToastLevel level,bool isShow)
        {
            Console.WriteLine(DateTime.Now);
            Onchange!.Invoke(new Notify() { message=message,level=level,isShow = isShow});
            await Task.Delay(5000).ContinueWith(s=>{
                Console.WriteLine(DateTime.Now);
                Onchange!.Invoke(new Notify() { message = message, level = level, isShow = false });
            });
        }




    }
    public class Notify
    {
        public string message;
        public ToastLevel level;
        public bool isShow;
    }

}
