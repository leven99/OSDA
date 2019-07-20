using OSerialPort.ViewModels;
using System;

namespace OSerialPort.Models
{
    class TimerM
    {
        public string SystemTimeData()
        {
            string SystemTime = "";
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
    }
}
