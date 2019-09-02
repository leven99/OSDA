using OSerialPort.Interface;
using OSerialPort.Models;
using System;
using System.Globalization;
using System.IO.Ports;
using System.Windows.Media;
using System.Windows.Threading;

namespace OSerialPort.ViewModels
{
    class MainWindowVM : MainWindowBase
    {
        #region 字段定义
        public SerialPort SPserialPort = null;
        public TimerM TimerM = null;
        #endregion

        #region 菜单栏帮助项
        public string VerInfo { get; set; }
        public string VerUpInfo { get; set; }
        public string ObjRP { get; set; }
        public string ObjIssue { get; set; }
        #endregion

        #region 串口配置区
        public string[] LSPPort { get; set; }
        public int[] LSPBaudRate { get; set; }
        public int[] LSPDataBits { get; set; }
        public string[] LSPStopBits { get; set; }
        public string[] LSPParity { get; set; }

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

        public string _SPStopBits;
        public string SPStopBits
        {
            get
            {
                return _SPStopBits;
            }
            set
            {
                if(_SPStopBits != value)
                {
                    _SPStopBits = value;
                    RaisePropertyChanged("SPStopbit");
                }
            }
        }

        public string _SPParity;
        public string SPParity
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
        public int _ReceDataCount;
        public int ReceDataCount
        {
            get
            {
                return _ReceDataCount;
            }
            set
            {
                if (_ReceDataCount != value)
                {
                    _ReceDataCount = value;
                    RaisePropertyChanged("ReceDataCount");
                }
            }
        }

        public string _ReceAutoSave;
        public string ReceAutoSave
        {
            get
            {
                return _ReceAutoSave;
            }
            set
            {
                if (_ReceAutoSave != value)
                {
                    _ReceAutoSave = value;
                    RaisePropertyChanged("ReceAutoSave");
                }
            }
        }

        public string _ReceHeader;
        public string ReceHeader
        {
            get
            {
                return _ReceHeader;
            }
            set
            {
                if (_ReceHeader != value)
                {
                    _ReceHeader = value;
                    RaisePropertyChanged("ReceHeader");
                }
            }
        }

        public ITextBoxAppend _ReceData;
        public ITextBoxAppend ReceData
        {
            get
            {
                return _ReceData;
            }
            set
            {
                if (_ReceData != value)
                {
                    _ReceData = value;
                    RaisePropertyChanged("ReceData");
                }
            }
        }
        #endregion

        #region 发送区
        public string _SendHeader;
        public string SendHeader
        {
            get
            {
                return _SendHeader;
            }
            set
            {
                if (_SendHeader != value)
                {
                    _SendHeader = value;
                    RaisePropertyChanged("SendHeader");
                }
            }
        }

        public int _SendDataCount;
        public int SendDataCount
        {
            get
            {
                return _SendDataCount;
            }
            set
            {
                if (_SendDataCount != value)
                {
                    _SendDataCount = value;
                    RaisePropertyChanged("SendDataCount");
                }
            }
        }

        public string _SendData;
        public string SendData
        {
            get
            {
                return _SendData;
            }
            set
            {
                if (_SendData != value)
                {
                    _SendData = value;
                    RaisePropertyChanged("SendData");
                }
            }
        }
        #endregion

        #region 辅助区
        public bool _HexRece;
        public bool HexRece
        {
            get
            {
                return _HexRece;
            }
            set
            {
                if(_HexRece != value)
                {
                    _HexRece = value;
                    RaisePropertyChanged("HexRece");
                }
            }
        }

        public bool _HexSend;
        public bool HexSend
        {
            get
            {
                return _HexSend;
            }
            set
            {
                if(_HexSend != value)
                {
                    _HexSend = value;
                    RaisePropertyChanged("HexSend");

                    if(HexSend == true)
                    {
                        DepictInfo = "请输入十六进制数据用空格隔开，比如0A 1B 2C 3D";
                    }
                    else
                    {
                        DepictInfo = "串行端口调试助手";
                    }
                }
            }
        }

        public bool _AutoSend;
        public bool AutoSend
        {
            get
            {
                return _AutoSend;
            }
            set
            {
                if (SPserialPort != null && SPserialPort.IsOpen)
                {
                    if (_AutoSend != value)
                    {
                        _AutoSend = value;
                        RaisePropertyChanged("AutoSend");
                    }

                    if (AutoSend == true)
                    {
                        StartAutoSendTimer(AutoSendNum);
                    }
                    else
                    {
                        StopAutoSendTimer();
                    }
                }
            }
        }

        public int _AutoSendNum;
        public int AutoSendNum
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

        public bool _SaveRece;
        public bool SaveRece
        {
            get
            {
                return _SaveRece;
            }
            set
            {
                if(_SaveRece != value)
                {
                    _SaveRece = value;
                    RaisePropertyChanged("SaveRece");
                }
            }
        }

        public void SaveRecePath()
        {

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
                if (_DepictInfo != value)
                {
                    _DepictInfo = value;
                    RaisePropertyChanged("DepictInfo");
                }
            }
        }

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
                    RaisePropertyChanged("SystemTime");
                }
            }
        }
        #endregion

        #region 打开/关闭串口实现

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
                case "Odd": parity = Parity.Odd; break;
                case "Even": parity = Parity.Even; break;
                case "Mark": parity = Parity.Mark; break;
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
                    StopBits = GetStopBits(SPStopBits.ToString()),
                    Parity = GetParity(SPParity.ToString()),
                    Handshake = Handshake.None,   /* 硬件控制流：无 */
                    RtsEnable = true   /* 启用请求发送（RTS）信号 */
                };

                SPserialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

                SPserialPort.Open();
                
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

        #region 接收实现
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            string recvData = sp.ReadExisting();

            if (HexRece)
            {
                foreach(char tmp in recvData.ToCharArray())
                {
                    ReceData.Append(string.Format("{0:X2} ", Convert.ToInt32(tmp)));
                }
            }
            /* 字符串接收 */
            else
            {
                ReceData.Append(recvData);
            } 

            if (SaveRece)
            {
                ReceAutoSave = "保存中";

                /* 执行数据保存函数 */
            }
            else
            {
                ReceAutoSave = "已停止";
            }

            ReceDataCount += recvData.Length;

            ReceHeader = "接收区：已接收" + ReceDataCount + "字节，接收自动保存[" + ReceAutoSave + "]";
        }
        #endregion

        #region 发送/多项发送实现
        public void Send()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                if (HexSend)
                {
                    int cnt = 0;
                    string[] _sendData = SendData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    char[] sendData = new char[_sendData.Length];

                    foreach(var tmp in _sendData)
                    {
                        sendData[cnt++] = (char)Int16.Parse(tmp, NumberStyles.AllowHexSpecifier);
                    }

                    SendDataCount += cnt;
                    SPserialPort.Write(sendData, 0, cnt);

                }
                /* 字符串发送 */
                else
                {
                    SendDataCount += SendData.Length;
                    SPserialPort.Write(SendData.ToCharArray(), 0, SendData.Length);
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
                /* 字符串发送 */
                else
                {

                }

                SendDataCount += SendData.Length;
            }
        }
        #endregion

        #region 清发送区/清接收区/清空计数实现
        public void ClarReceData()
        {
            ReceData.Delete();
        }

        public void ClearSendData()
        {
            SendData = string.Empty;
        }

        public void ClearCount()
        {
            ReceDataCount = 0;
            ReceHeader = "接收区：已接收" + ReceDataCount + "字节，接收自动保存[" + ReceAutoSave + "]";

            SendDataCount = 0;
        }
        #endregion

        #region 定时器实现
        DispatcherTimer SystemDispatcherTimer = new DispatcherTimer();   /* 系统定时器 */

        public void InitSystemClockTimer()
        {
            SystemDispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            SystemDispatcherTimer.IsEnabled = true;
            SystemDispatcherTimer.Tick += SystemDispatcherTimer_Tick;
            SystemDispatcherTimer.Start();
        }

        public void SystemDispatcherTimer_Tick(object sender, EventArgs e)
        {
            SystemTime = TimerM.SystemTimeData();
        }

        public DispatcherTimer AutoSendDispatcherTimer = new DispatcherTimer();   /* 自动发送定时器 */

        public void InitAutoSendTimer()
        {
            AutoSendDispatcherTimer.IsEnabled = false;
            AutoSendDispatcherTimer.Tick += AutoSendDispatcherTimer_Tick;
        }

        public void AutoSendDispatcherTimer_Tick(object sender, EventArgs e)
        {
            Send();
        }

        public void StartAutoSendTimer(int interval)
        {
            AutoSendDispatcherTimer.IsEnabled = true;
            AutoSendDispatcherTimer.Interval = TimeSpan.FromMilliseconds(interval);
            AutoSendDispatcherTimer.Start();
        }

        public void StopAutoSendTimer()
        {
            AutoSendDispatcherTimer.IsEnabled = false;
            AutoSendDispatcherTimer.Stop();
        }
        #endregion

        public MainWindowVM()
        {
            /* 菜单栏帮助项 */
            VerInfo   = "OSerialPort v1.1.0";
            VerUpInfo = "检查更新";
            ObjRP     = "Gitee存储库";
            ObjIssue  = "报告问题";

            /* 串口配置区 */
            LSPPort     = SerialPort.GetPortNames();
            LSPBaudRate = new int[] { 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600};
            LSPDataBits = new int[] { 5, 6, 7, 8 };
            LSPStopBits = new string[] { "One", "Two", "OnePointFive" };
            LSPParity   = new string[] { "None", "Odd", "Even", "Mark", "Space" };

            SPBaudRate  = 9600;
            SPDataBits  = 8;
            SPStopBits  = "One";
            SPParity    = "None";
            SPBrush     = Brushes.Red;
            OpenCloseSP = "打开串口";

            /* 接收区 */
            ReceData = new IClassTextBoxAppend();
            ReceDataCount = 0;
            ReceAutoSave  = "已停止";
            ReceHeader    = "接收区：已接收" + ReceDataCount + "字节，接收自动保存[" + ReceAutoSave + "]";

            /* 发送区 */
            SendData      = String.Empty;
            SendDataCount = 0;
            InitAutoSendTimer();

            /* 辅助 */
            HexRece     = false;
            HexSend     = false;
            AutoSend    = false;
            SaveRece    = false;
            AutoSendNum = 1000;

            /* 状态栏 */
            DepictInfo = "串行端口调试助手";
            SystemTime = "2019年06月09日 12:13:15";
            TimerM = new TimerM();
            InitSystemClockTimer();
        }
    }
}
