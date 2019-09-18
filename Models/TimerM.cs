using OSDA.ViewModels;
using System;
using System.Windows.Threading;

namespace OSDA.Models
{
    public class TimerModel : MainWindowBase
    {
        /// <summary>
        /// 状态栏 - 系统时间
        /// </summary>
        string _SystemTime;
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

        readonly DispatcherTimer SystemDispatcherTimer = new DispatcherTimer();

        public void InitSystemClockTimer()
        {
            SystemDispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            SystemDispatcherTimer.IsEnabled = true;
            SystemDispatcherTimer.Tick += SystemDispatcherTimer_Tick;
            SystemDispatcherTimer.Start();
        }

        public void SystemDispatcherTimer_Tick(object sender, EventArgs e)
        {
            SystemTime = SystemTimeData();
        }

        public static string SystemTimeData()
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
