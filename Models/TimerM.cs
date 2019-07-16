using OSerialPort.ViewModels;
using System;
using System.Windows.Threading;

namespace OSerialPort.Models
{
    class TimerM
    {
        DispatcherTimer ADispatcherTimer = new DispatcherTimer();

        /// <summary>
        /// 定时器 - 用于自动发送
        /// </summary>
        public void InitAutoClockTimer()
        {
            ADispatcherTimer.IsEnabled = false;
            ADispatcherTimer.Tick += DispatcherTimer_ATick;
            ADispatcherTimer.Start();
        }

        private void DispatcherTimer_ATick(object sender, EventArgs e)
        {
            
        }

        public void StartAutoSendDataTimer(int interval)
        {
            ADispatcherTimer.IsEnabled = true;
            ADispatcherTimer.Interval = TimeSpan.FromMilliseconds(interval);   /* 毫秒 */
            ADispatcherTimer.Start();
        }

        public void StopAutoSendDataTimer()
        {
            ADispatcherTimer.IsEnabled = false;
            ADispatcherTimer.Stop();
        }
    }
}
