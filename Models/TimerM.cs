using OSDA.ViewModels;
using System;
using System.Timers;

namespace OSDA.Models
{
    internal class TimerModel : MainWindowBase
    {
        #region 字段
        private static Timer SystemTimer = null;   /* 该对象持续存在于整个应用程序运行期间 */
        #endregion

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

        private string SystemTimeData()
        {
            DateTime systemTime = DateTime.Now;

            return string.Format(cultureInfo, "{0}年{1}月{2}日 {3}:{4}:{5}",
                systemTime.Year.ToString("0000", cultureInfo),
                systemTime.Month.ToString("00", cultureInfo),
                systemTime.Day.ToString("00", cultureInfo),
                systemTime.Hour.ToString("00", cultureInfo),
                systemTime.Minute.ToString("00", cultureInfo),
                systemTime.Second.ToString("00", cultureInfo));
        }

        public void TimerDataContext()
        {
            SystemTime = string.Format(cultureInfo, "2019年08月31日 12:13:15");

            InitSystemClockTimer();
        }
    }
}
