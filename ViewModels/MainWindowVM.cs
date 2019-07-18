using OSerialPort.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Media;
using System.Windows.Threading;

namespace OSerialPort.ViewModels
{
    class MainWindowVM : MainWindowBase
    {
        private SerialPort SPserialPort = null;

        #region 串口集
        public string[] LSPPort { get; set; }
        public int[] LSPBaudRate { get; set; }
        public int[] LSPDataBits { get; set; }
        public string[] LSPStopbits { get; set; }
        public string[] LSPParity { get; set; }
        #endregion

        #region 串口配置
        public string _SPPort;
        public string SPPort
        {
            get
            {
                return _SPPort;
            }
            set
            {
                if(_SPPort != value)
                {
                    _SPPort = value;
                    RaisePropertyChanged("SPPort");
                }
            }
        }

        public int _SPBaudRate;
        public int SPBaudRate
        {
            get
            {
                return _SPBaudRate;
            }
            set
            {
                if(_SPBaudRate != value)
                {
                    _SPBaudRate = value;
                    RaisePropertyChanged("SPBaudRate");
                }
            }
        }

        public int _SPDataBits;
        public int SPDataBits
        {
            get
            {
                return _SPDataBits;
            }
            set
            {
                if(_SPDataBits != value)
                {
                    _SPDataBits = value;
                    RaisePropertyChanged("SPDataBits");
                }
            }
        }

        public StopBits _SPStopbits;
        public StopBits SPStopbits
        {
            get
            {
                return _SPStopbits;
            }
            set
            {
                if(_SPStopbits != value)
                {
                    _SPStopbits = value;
                    RaisePropertyChanged("SPStopbit");
                }
            }
        }

        public Parity _SPParity;
        public Parity SPParity
        {
            get
            {
                return _SPParity;
            }
            set
            {
                if(_SPParity != value)
                {
                    _SPParity = value;
                    RaisePropertyChanged("SPParity");
                }
            }
        }

        public Brush _SPBrush;
        public Brush SPBrush
        {
            get
            {
                return _SPBrush;
            }
            set
            {
                if(_SPBrush != value)
                {
                    _SPBrush = value;
                    RaisePropertyChanged("SPBrush");
                }
            }
        }

        public string _OpenCloseSP;
        public string OpenCloseSP
        {
            get
            {
                return _OpenCloseSP;
            }
            set
            {
                if (_OpenCloseSP != value)
                {
                    _OpenCloseSP = value;
                    RaisePropertyChanged("OpenCloseSP");
                }
            }
        }
        #endregion

        #region 接收区
        public string ReceHeadrt { get; set; }

        public UInt32 ReceDataCount { get; set; }

        public string ReceAutoSave { get; set; }

        public string ReceData { get; set; }
        #endregion

        #region 发送区
        public string SendHeader { get; set; }

        public UInt32 SendDataCount { get; set; }

        public string SendData { get; set; }
        #endregion

        #region 辅助
        public bool HexRece { get; set; }

        public bool HexSend { get; set; }

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
                if (_AutoSendNum != value)
                {
                    _AutoSendNum = value;
                    RaisePropertyChanged("AutoSendNum");
                }
            }
        }

        public bool SaveRece { get; set; }

        /// <summary>
        /// 路径选择 - 用于修改接收接收时保存数据的路径
        /// </summary>
        public void SaveRecePath()
        {
            if (SaveRece)
            {

            }
            else
            {

            }
        }
        #endregion

        #region 清空
        public void ClarReceData()
        {
            ReceData = string.Empty;

        }

        public void ClearSendData()
        {
            SendData = string.Empty;
        }

        public void ClearCount()
        {
            ReceDataCount = 0;
            SendDataCount = 0;
        }
        #endregion

        #region 打开/关闭串口

        #region 处理停止位和校验位
        public StopBits GetStopBits(string emp)
        {
            StopBits stopBits = StopBits.One;
            switch (emp)
            {
                case "One": stopBits = StopBits.One; break;
                case "Two": stopBits = StopBits.Two; break;
                case "OnePointFive": stopBits = StopBits.OnePointFive; break;
                default: break;
            }
            return stopBits;
        }

        public Parity GetParity(string emp)
        {
            Parity parity = Parity.None;
            switch (emp)
            {
                case "None": parity = Parity.None; break;
                case "Even": parity = Parity.Even; break;
                case "Mark": parity = Parity.Mark; break;
                case "Odd": parity = Parity.Odd; break;
                case "Space": parity = Parity.Space; break;
                default: break;
            }
            return parity;
        }
        #endregion

        public bool OpenSP()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                return CloseSP();
            }

            try
            {
                SPserialPort = new SerialPort
                {
                    PortName = SPPort,
                    BaudRate = SPBaudRate,
                    DataBits = SPDataBits,
                    StopBits = GetStopBits(SPStopbits.ToString()),
                    Parity = GetParity(SPParity.ToString())
                };

                SPserialPort.Open();
                SPserialPort.DiscardInBuffer();   /* 串口必须打开，才能清除缓冲区数据 */
                SPserialPort.DiscardOutBuffer();

                if (SPserialPort.IsOpen)
                {
                    SPBrush = Brushes.GreenYellow;
                    OpenCloseSP = "关闭串口";
                    DepictInfo = string.Format("成功打开串行端口{0}、波特率{1}、数据位{2}、停止位{3}、校验位{4}", SPserialPort.PortName,
                    SPserialPort.BaudRate.ToString(), SPserialPort.DataBits.ToString(),
                    SPserialPort.StopBits.ToString(), SPserialPort.Parity.ToString());

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

                    SPBrush = Brushes.Red;
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
        #endregion

        #region 发送/多项发送
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
        #endregion

        #region 状态栏
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

        #region 计时器初始化 - 用于系统时间显示
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
        #endregion

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

        /// <summary>
        /// 获取系统当前时间和日期信息
        /// </summary>
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
        #endregion

        public MainWindowVM()
        {
            LSPPort = SerialPort.GetPortNames();
            LSPBaudRate = new int[] { 1200, 2400, 4800, 9600, 14400, 19200, 38400, 57600, 115200 };
            LSPDataBits = new int[] { 5, 6, 7, 8 };
            LSPStopbits = new string[] { "One", "Two", "OnePointFive" };
            LSPParity = new string[] { "None", "Even", "Odd", "Mark", "Space" };

            SPBrush = Brushes.Red;
            OpenCloseSP = "打开串口";

            ReceDataCount = 0;
            ReceAutoSave  = "已停止";
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
