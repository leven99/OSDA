using System;
using System.Windows.Threading;

namespace OSerialPort.Models
{
    class Timer
    {
        ViewModels.MainWindow VMmainWindow = new ViewModels.MainWindow();

        /// <summary>
        /// 定时器注册
        /// </summary>
        public void InitClockTimer()
        {
            DispatcherTimer DTDispatcherTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1),
                IsEnabled = true
            };
            DTDispatcherTimer.Tick += ClockTimer_Tick;
            DTDispatcherTimer.Start();
        }

        public void ClockTimer_Tick(object sender, EventArgs e)
        {
            VMmainWindow.SystemTimeData();
        }
    }
}
