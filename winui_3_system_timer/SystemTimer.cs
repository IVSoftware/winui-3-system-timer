using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace winui_3_system_timer
{
    static class SystemTimer
    {
        static SystemTimer()
        {
            Task.Run(async () =>
            {
                while (!_dispose)
                {
                    PushDT(DateTime.Now);
                    await Task.Delay(100);
                }
            });
        }

        public static void PushDT(DateTime now)
        {
            // Using a 'now' that doesn't change within this method:
            Second = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Kind);
        }
        public static event PropertyChangedEventHandler PropertyChanged;
        static DateTime _second = DateTime.MinValue;
        public static DateTime Second
        {
            get => _second;
            set
            {
                if (_second != value)
                {
                    _second = value;
                    PropertyChanged?.Invoke(nameof(SystemTimer), new PropertyChangedEventArgs(nameof(Second)));
                }
            }
        }
        static bool _dispose = false;
    }
}