using OSerialPort.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Threading;

namespace OSerialPort.ViewModels
{
    class MainWindowVM : MainWindowBase
    {
        private SerialPort SPserialPort = null;

        /// <summary>
        /// 串口配置
        /// </summary>
        public string[] SPPort { get; set; }
        public List<int> SPBaudRate { get; set; }
        public List<int> SPDataBits { get; set; }
        public List<int> SPStopbit { get; set; }
        public List<string> SPParity { get; set; }

        /// <summary>
        /// 接收区数据
        /// </summary>
        public string ReceData { get; set; }

        public void ClarReceData()
        {
            ReceData = string.Empty;

        }

        /// <summary>
        /// 发送区数据
        /// </summary>
        public string SendData { get; set; }

        public void ClearSendData()
        {
            SendData = string.Empty;
        }

        /// <summary>
        /// 接收区
        /// </summary>
        public string ReceHeadrt { get; set; }

        public UInt32 ReceDataCount { get; set; }

        public string ReceAutoSave { get; set; }

        /// <summary>
        /// 发送区
        /// </summary>
        public string SendHeader { get; set; }

        public UInt32 SendDataCount { get; set; }

        /// <summary>
        /// 清空计数（接收区字节计数和发送区字节计数同时清空）
        /// </summary>
        public void ClearCount()
        {
            ReceDataCount = 0;
            SendDataCount = 0;
        }

        /// <summary>
        /// 打开串口/发送串口
        /// </summary>
        public string _OpenCloseSP;
        public string OpenCloseSP
        {
            get
            {
                return _OpenCloseSP;
            }
            set
            {
                if(_OpenCloseSP != value)
                {
                    _OpenCloseSP = value;
                    RaisePropertyChanged("OpenCloseSP");
                }
            }
        }

        public bool OpenSP()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                return CloseSP();
            }

            try
            {
                if (SPserialPort.IsOpen)
                {
                    OpenCloseSP = "关闭串口";
                    DepictInfo = "串行端口打开成功";

                    return true;
                }
                else
                {
                    DepictInfo = "串行端口打开失败";

                    return false;
                }
            }
            catch
            {
                DepictInfo = "串行端口发生意外，打开失败，请检查线路";

                return false;
            }
        }

        public bool CloseSP()
        {
            try
            {
                if (SPserialPort.IsOpen)
                {
                    SPserialPort.Close();

                    OpenCloseSP = "打开串口";
                    DepictInfo = "串行端口关闭成功";

                    return SPserialPort.IsOpen;
                }
                else
                {
                    DepictInfo = "串行端口已关闭";

                    return SPserialPort.IsOpen;
                }
            }
            catch
            {
                DepictInfo = "串行端口发生意外，关闭失败，请检查线路";

                return false;
            }
        }

        /// <summary>
        /// 16进制接收/发送
        /// </summary>
        public bool HexRece { get; set; }

        public bool HexSend { get; set; }

        /// <summary>
        /// 自动发送
        /// </summary>
        public bool AutoSend { get; set; }

        public UInt32 _AutoSendNum;
        public UInt32 AutoSendNum
        {
            get
            {
                return _AutoSendNum;
            }
            set
            {
                if(_AutoSendNum != value)
                {
                    _AutoSendNum = value;
                    RaisePropertyChanged("AutoSendNum.ToString()");
                }
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        public void Send()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                if (HexSend)
                {

                }
                else
                {

                }
            }
        }

        public void Sends()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                if (HexSend)
                {

                }
                else
                {

                }
            }
        }

        /// <summary>
        /// 保存接收（默认保存在安装目录下以日期命名的.txt文本文件）
        /// </summary>
        public bool SaveRece { get; set; }

        public void SaveRecePath()
        {
            if (SaveRece)
            {

            }
            else
            {

            }
        }

        /// <summary>
        /// 描述信息
        /// </summary>
        string _DepictInfo;
        public string DepictInfo
        {
            get
            {
                return _DepictInfo;
            }
            set
            {
                if(_DepictInfo != value)
                {
                    _DepictInfo = value;
                    RaisePropertyChanged("DepictInfo");
                }
            }
        }

        /// <summary>
        /// 系统时间
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
                if(_SystemTime != value)
                {
                    _SystemTime = value;
                    RaisePropertyChanged("SystemTime");
                }
            }
        }

        public void InitSystemClockTimer()
        {
            DispatcherTimer SDispatcherTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1),   /* 秒 */
                IsEnabled = true
            };
            SDispatcherTimer.Tick += DispatcherTimer_STick;
            SDispatcherTimer.Start();
        }

        private void DispatcherTimer_STick(object sender, EventArgs e)
        {
            SystemTimeData();
        }

        public void SystemTimeData()
        {
            DateTime systemTime = DateTime.Now;

            SystemTime = string.Format("{0}年{1}月{2}日 {3}:{4}:{5}",
                systemTime.Year.ToString("0000"),
                systemTime.Month.ToString("00"),
                systemTime.Day.ToString("00"),
                systemTime.Hour.ToString("00"),
                systemTime.Minute.ToString("00"),
                systemTime.Second.ToString("00"));
        }

        /// <summary>
        /// UI初始化
        /// </summary>
        public MainWindowVM()
        {
            SPPort = SerialPort.GetPortNames();
            SPBaudRate = new List<int> { 1200, 2400, 4800, 9600, 14400, 19200, 38400, 57600, 115200 };
            SPDataBits = new List<int> { 5, 6, 7, 8 };
            SPStopbit = new List<int> { 1, 2 };
            SPParity = new List<string> { "None", "Even", "Odd", "Mark", "Space" };

            OpenCloseSP = "打开串口";

            ReceDataCount = 0;
            ReceAutoSave = "已停止";
            SendDataCount = 0;

            ReceHeadrt = "接收区：已接收" + ReceDataCount + "字节，接收自动保存[" + ReceAutoSave + "]";
            SendHeader = "发送区：已发送" + SendDataCount + "字节";

            AutoSendNum = 1000;

            DepictInfo = "串行端口调试助手";
            SystemTime = "2019年06月09日 12:13:15";
            InitSystemClockTimer();   /* 实时显示系统时间 */
        }
    }
}
