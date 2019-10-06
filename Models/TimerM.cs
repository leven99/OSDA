using OSDA.ViewModels;
using System;
using System.Timers;

namespace OSDA.Models
{
    public class TimerModel : MainWindowBase
    {
        /// <summary>
        /// 状态栏 - 系统时间
        /// </summary>
        private string _SystemTime;
        public string SystemTime
        {
            get
            {
                return _SystemTime;
            }
            set
            {
                if (_SystemTime != value)
                {
                    _SystemTime = value;
                    RaisePropertyChanged(nameof(SystemTime));
                }
            }
        }

        private Timer SystemTimer = null;

        public void InitSystemClockTimer()
        {
            SystemTimer = new Timer
            {
                Interval = 1000
            };

            SystemTimer.Elapsed += SystemTimer_Elapsed;
            SystemTimer.AutoReset = true;
            SystemTimer.Enabled = true;
        }

        private void SystemTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SystemTime = SystemTimeData();
        }

        private static string SystemTimeData()
        {
            string SystemTime;
            DateTime systemTime = DateTime.Now;

            SystemTime = string.Format("{0}年{1}月{2}日 {3}:{4}:{5}",
                systemTime.Year.ToString("0000"),
                systemTime.Month.ToString("00"),
                systemTime.Day.ToString("00"),
                systemTime.Hour.ToString("00"),
                systemTime.Minute.ToString("00"),
                systemTime.Second.ToString("00"));

            return SystemTime;
        }

        public void TimerDataContext()
        {
            SystemTime = "2019年08月31日 12:13:15";

            InitSystemClockTimer();
        }
    }
}
