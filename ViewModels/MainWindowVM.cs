using OSerialPort.Models;
using OSerialPort.Views;
using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;

namespace OSerialPort.ViewModels
{
    class MainWindowViewModel : MainWindowBase, IDisposable
    {
        public SerialPort SPserialPort = new SerialPort();

        public HelpModel HelpModel { get; set; }
        public RecvModel RecvModel { get; set; }
        public SendModel SendModel { get; set; }
        public SerialPortModel SerialPortModel { get; set; }
        public TimerModel TimerModel { get; set; }

        public string _DepictInfo;
        public string DepictInfo
        {
            get { return _DepictInfo; }
            set
            {
                if (_DepictInfo != value)
                {
                    _DepictInfo = value;
                    RaisePropertyChanged(nameof(DepictInfo));
                }
            }
        }

        #region 菜单栏

        #region 选项 - 字节编码
        public void ASCIIEnable()
        {
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF16Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.ASCIIEnable = true;

            try
            {
                if(SerialPortModel.ASCIIEnable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.ASCII;
                }
            }
            catch(ArgumentException e)
            {
                DepictInfo = string.Format("更改字节编码为{0}是非法操作", e.ParamName);
            }
        }

        public void UTF8Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF16Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.UTF8Enable = true;

            try
            {
                if (SerialPortModel.UTF8Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.UTF8;
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = string.Format("更改字节编码为{0}是非法操作", e.ParamName);
            }
        }

        public void UTF16Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.UTF16Enable = true;

            try
            {
                if (SerialPortModel.UTF16Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.Unicode;
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = string.Format("设置字节编码为{0}是非法操作", e.ParamName);
            }
        }

        public void UTF32Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF16Enable = false;

            SerialPortModel.UTF32Enable = true;

            try
            {
                if (SerialPortModel.UTF32Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.UTF32;
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = string.Format("更改字节编码为{0}是非法操作", e.ParamName);
            }
        }
        #endregion

        public void RtsEnable()
        {
            SerialPortModel.RtsEnable = !(SerialPortModel.RtsEnable);

            try
            {
                if(SerialPortModel.RtsEnable)
                {
                    SPserialPort.RtsEnable = true;
                }
                else
                {
                    SPserialPort.RtsEnable = false;
                }
            }
            catch(InvalidOperationException e)
            {
                DepictInfo = string.Format("[{0}]当设置为硬件流或硬软件流时，不允许设置RTS", e.HResult.ToString("X"));
            }
            catch (IOException e)
            {
                DepictInfo = string.Format("[{0}]端口处于无效状态", e.HResult.ToString("X"));
            }
        }

        public void DtrEnable()
        {
            SerialPortModel.DtrEnable = !(SerialPortModel.DtrEnable);

            try
            {
                if (SerialPortModel.DtrEnable)
                {
                    SPserialPort.DtrEnable = true;
                }
                else
                {
                    SPserialPort.DtrEnable = false;
                }
            }
            catch (InvalidOperationException e)
            {
                DepictInfo = string.Format("[{0}]当设置为硬件流或硬软件流时，不允许设置DTR", e.HResult.ToString("X"));
            }
            catch (IOException e)
            {
                DepictInfo = string.Format("[{0}]端口处于无效状态", e.HResult.ToString("X"));
            }
        }

        #region 选项 - 流控制
        public void NoneEnable()
        {
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.NoneEnable = true;

            try
            {
                if(SerialPortModel.NoneEnable)
                {
                    SPserialPort.Handshake = Handshake.None;
                }
            }
            catch (IOException e)
            {
                DepictInfo = string.Format("[{0}]端口处于无效状态", e.HResult.ToString("X"));
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format("设置流控制为{0}是非法操作", e.ParamName);
            }
        }

        public void RequestToSendEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.RequestToSendEnable = true;

            try
            {
                if (SerialPortModel.RequestToSendEnable)
                {
                    SPserialPort.Handshake = Handshake.RequestToSend;
                }
            }
            catch (IOException e)
            {
                DepictInfo = string.Format("[{0}]端口处于无效状态", e.HResult.ToString("X"));
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format("设置流控制为{0}是非法操作", e.ParamName);
            }
        }

        public void XOnXOffEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.RequestToSendEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.XOnXOffEnable = true;

            try
            {
                if (SerialPortModel.XOnXOffEnable)
                {
                    SPserialPort.Handshake = Handshake.XOnXOff;
                }
            }
            catch (IOException e)
            {
                DepictInfo = string.Format("[{0}]端口处于无效状态", e.HResult.ToString("X"));
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format("设置流控制为{0}是非法操作", e.ParamName);
            }
        }

        public void RequestToSendXOnXOffEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendEnable = false;

            SerialPortModel.RequestToSendXOnXOffEnable = true;

            try
            {
                if (SerialPortModel.RequestToSendXOnXOffEnable)
                {
                    SPserialPort.Handshake = Handshake.RequestToSendXOnXOff;
                }
            }
            catch (IOException e)
            {
                DepictInfo = string.Format("[{0}]端口处于无效状态", e.HResult.ToString("X"));
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format("设置流控制为{0}是非法操作", e.ParamName);
            }
        }
        #endregion

        /// <summary>
        /// 帮助 - 检查更新
        /// </summary>
        public async void UpdateAsync()
        {
            HelpModel.UpdateVerInfoNumber = await HelpModel.UpdateInfoAsync();

            /* _HttpRequestException 是 UpdateInfoAsync() 返回的自定义字符串 */
            if (HelpModel.UpdateVerInfoNumber == "_HttpRequestException")
            {
                DepictInfo = string.Format("网络连接失败，请检查网络或稍后重试......");
            }
            else
            {
                if (HelpModel.UpdateVerInfoNumber == HelpModel.VerInfoNumber)
                {
                    DepictInfo = "OSerialPort v" + HelpModel.VerInfoNumber + "已经是最新版";
                }
                else
                {
                    Thread wPFUpdateThread = new Thread(new ThreadStart(ThreadStartingWPFUpdate));
                    wPFUpdateThread.SetApartmentState(ApartmentState.STA);
                    wPFUpdateThread.IsBackground = true;
                    wPFUpdateThread.Start();
                }
            }
        }

        private void ThreadStartingWPFUpdate()
        {
            WPFUpdate wPFUpdate = new WPFUpdate();
            wPFUpdate.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
        #endregion

        #region 打开/关闭串口
        public bool OpenSP()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                return CloseSP();
            }

            try
            {
                SPserialPort.PortName = SerialPortModel.SPPort;
                SPserialPort.BaudRate = SerialPortModel.SPBaudRate;
                SPserialPort.DataBits = SerialPortModel.SPDataBits;
                SPserialPort.StopBits = SerialPortModel.GetStopBits(SerialPortModel.SPStopBits.ToString());
                SPserialPort.Parity = SerialPortModel.GetParity(SerialPortModel.SPParity.ToString());

                SPserialPort.WriteBufferSize = 1048576;   /* 输出缓冲区的大小为1048576字节 = 1MB */
                SPserialPort.ReadBufferSize = 2097152;    /* 输入缓冲区的大小为2097152字节 = 2MB */

                /* 字节编码 */
                if (SerialPortModel.ASCIIEnable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.ASCII;
                }
                else if (SerialPortModel.UTF8Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.UTF8;
                }
                else if (SerialPortModel.UTF16Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.Unicode;
                }
                else if (SerialPortModel.UTF32Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.UTF32;
                }

                /* 发送请求（RTS）信号 */
                if (SerialPortModel.RtsEnable)
                {
                    SPserialPort.RtsEnable = true;
                }
                else
                {
                    SPserialPort.RtsEnable = false;
                }

                /* 数据终端就绪（DTR）信号 */
                if (SerialPortModel.DtrEnable)
                {
                    SPserialPort.DtrEnable = true;
                }
                else
                {
                    SPserialPort.DtrEnable = false;
                }

                /* 流控制 */
                if (SerialPortModel.NoneEnable)
                {
                    SPserialPort.Handshake = Handshake.None;
                }
                else if (SerialPortModel.RequestToSendEnable)
                {
                    SPserialPort.Handshake = Handshake.RequestToSend;
                }
                else if (SerialPortModel.XOnXOffEnable)
                {
                    SPserialPort.Handshake = Handshake.XOnXOff;
                }
                else if (SerialPortModel.RequestToSendXOnXOffEnable)
                {
                    SPserialPort.Handshake = Handshake.RequestToSendXOnXOff;
                }

                /* 数据接收事件 */
                SPserialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

                /* 信号状态事件 */
                SPserialPort.PinChanged += new SerialPinChangedEventHandler(SerialPortModel.SerialPort_PinChanged);

                SPserialPort.Open();

                if (SPserialPort.IsOpen)
                {
                    SerialPortModel.SPBrush = Brushes.GreenYellow;
                    SerialPortModel.OpenCloseSP = "关闭串口";
                    DepictInfo = string.Format("成功打开串行端口{0}、波特率{1}、数据位{2}、停止位{3}、校验位{4}",
                        SPserialPort.PortName, SPserialPort.BaudRate.ToString(), SPserialPort.DataBits.ToString(),
                        SPserialPort.StopBits.ToString(), SPserialPort.Parity.ToString());

                    SerialPortModel.SPPortEnable = false;
                    SerialPortModel.SPBaudRateEnable = false;
                    SerialPortModel.SPDataBitsEnable = false;
                    SerialPortModel.SPStopBitsEnable = false;
                    SerialPortModel.SPParityEnable = false;

                    return true;
                }
                else
                {
                    DepictInfo = "串行端口打开失败";

                    return false;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                DepictInfo = string.Format("[{0}]端口访问被拒绝", e.HResult.ToString("X"));

                return false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format("串口属性{0}是非法的", e.ParamName);

                return false;
            }
            catch (ArgumentException e)
            {
                DepictInfo = string.Format("串口{0}不支持", e.ParamName);

                return false;
            }
            catch (IOException e)
            {
                DepictInfo = string.Format("[{0}]端口处于无效状态", e.HResult.ToString("X"));

                return false;
            }
            catch (InvalidOperationException e)
            {
                DepictInfo = string.Format("[{0}]指定端口已经打开", e.HResult.ToString("X"));

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

                    SerialPortModel.SPBrush = Brushes.Red;
                    SerialPortModel.OpenCloseSP = "打开串口";

                    DepictInfo = "串行端口关闭成功";

                    SerialPortModel.SPPortEnable = true;
                    SerialPortModel.SPBaudRateEnable = true;
                    SerialPortModel.SPDataBitsEnable = true;
                    SerialPortModel.SPStopBitsEnable = true;
                    SerialPortModel.SPParityEnable = true;

                    RecvModel.RecvAutoSave = "已停止";
                    RecvModel.RecvHeader = "接收区：已接收" + RecvModel.RecvDataCount +
                            "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]";

                    return SPserialPort.IsOpen;
                }
                else
                {
                    DepictInfo = "串行端口已关闭";

                    return SPserialPort.IsOpen;
                }
            }
            catch(IOException e)
            {
                DepictInfo = string.Format("[{0}]端口处于无效状态", e.HResult.ToString("X"));

                return false;
            }
        }
        #endregion

        #region 辅助区
        public bool _HexSend;
        public bool HexSend
        {
            get
            {
                return _HexSend;
            }
            set
            {
                if (_HexSend != value)
                {
                    _HexSend = value;
                    RaisePropertyChanged(nameof(HexSend));

                    if (HexSend == true)
                    {
                        DepictInfo = "请输入十六进制数据用空格隔开，比如A0 B1 C2 D3";
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
                        RaisePropertyChanged(nameof(AutoSend));
                    }

                    if (AutoSend == true)
                    {
                        if (SendModel.AutoSendNum <= 0)
                        {
                            DepictInfo = "请输入正确的发送时间间隔";
                            return;
                        }

                        StartAutoSendTimer(SendModel.AutoSendNum);
                    }
                    else
                    {
                        StopAutoSendTimer();
                    }
                }
            }
        }

        public bool _SaveRecv;
        public bool SaveRecv
        {
            get
            {
                return _SaveRecv;
            }
            set
            {
                if (_SaveRecv != value)
                {
                    _SaveRecv = value;
                    RaisePropertyChanged(nameof(SaveRecv));
                }

                if (SaveRecv)
                {
                    DepictInfo = "接收数据默认保存在程序基目录，可以点击路径选择操作更换";
                }
                else
                {
                    DepictInfo = "串行端口调试助手";
                    RecvModel.RecvAutoSave = "已停止";

                }
            }
        }
        #endregion

        #region 自动发送定时器实现
        public DispatcherTimer AutoSendDispatcherTimer = new DispatcherTimer();

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

        #region 发送
        public void Send()
        {
            try
            {
                if (SPserialPort != null && SPserialPort.IsOpen)
                {
                    Int32 SendCount = 0;

                    if (HexSend)
                    {
                        string[] _sendData = SendModel.SendData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        byte[] sendData = new byte[_sendData.Length];

                        foreach (var tmp in _sendData)
                        {
                            sendData[SendCount++] = byte.Parse(tmp, NumberStyles.AllowHexSpecifier);
                        }

                        SPserialPort.Write(sendData, 0, SendCount);

                    }
                    else
                    {
                        SendCount = SPserialPort.Encoding.GetByteCount(SendModel.SendData);
                        SPserialPort.Write(SPserialPort.Encoding.GetBytes(SendModel.SendData), 0, SendCount);
                    }

                    SendModel.SendDataCount += SendCount;
                }
            }
            catch
            {
                DepictInfo = "发送异常，请检查发送数据";
            }
        }
        #endregion

        #region 多项发送
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

                SendModel.SendDataCount += SendModel.SendData.Length;
            }
        }
        #endregion

        #region 路径选择
        public void SaveRecvPath()
        {
            RecvModel.RecvPath();
        }
        #endregion

        #region 清接收区
        public void ClarReceData()
        {
            RecvModel.RecvData.Delete();

            RecvModel.RecvDataDeleteCount = 1;
        }
        #endregion

        #region 清发送区
        public void ClearSendData()
        {
            SendModel.SendData = string.Empty;
        }
        #endregion

        #region 清空计数
        public void ClearCount()
        {
            RecvModel.RecvDataCount = 0;
            RecvModel.RecvHeader = "接收区：已接收" + RecvModel.RecvDataCount + "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]";

            SendModel.SendDataCount = 0;
        }
        #endregion

        #region 数据接收事件实现
        public void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort _SerialPort = (SerialPort)sender;

            int _bytesToRead = _SerialPort.BytesToRead;
            byte[] recvData = new byte[_bytesToRead];

            _SerialPort.Read(recvData, 0, _bytesToRead);

            if (RecvModel.HexRecv)
            {
                foreach (var tmp in recvData)
                {
                    RecvModel.RecvData.Append(string.Format("{0:X2} ", tmp));
                }
            }
            else
            {
                RecvModel.RecvData.Append(_SerialPort.Encoding.GetString(recvData));
            }

            if (SaveRecv)
            {
                RecvModel.RecvAutoSave = "保存中";

                SaveRecvData(_SerialPort.Encoding.GetString(recvData));
            }
            else
            {
                RecvModel.RecvAutoSave = "已停止";
            }

            RecvModel.RecvDataCount += recvData.Length;

            RecvModel.RecvHeader = "接收区：已接收" + RecvModel.RecvDataCount +
                "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]";

            if (RecvModel.RecvDataCount > (32768 * RecvModel.RecvDataDeleteCount))
            {
                RecvModel.RecvData.Delete();   /* 32MB */

                RecvModel.RecvDataDeleteCount += 1;
            }
        }

        public async void SaveRecvData(string ReceData)
        {
            try
            {
                if (RecvModel.DataRecePath == null)
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\ReceData\\");

                    using (StreamWriter DefaultReceDataPath = new StreamWriter(
                        AppDomain.CurrentDomain.BaseDirectory + "\\ReceData\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt",
                        true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData);
                    }
                }
                else
                {
                    using (StreamWriter DefaultReceDataPath = new StreamWriter(RecvModel.DataRecePath, true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData);
                    }
                }
            }
            catch
            {
                DepictInfo = "接收数据保存失败";

                RecvModel.RecvAutoSave = "已停止";
                RecvModel.RecvHeader = "接收区：已接收" + RecvModel.RecvDataCount + 
                    "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]";
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            HelpModel = new HelpModel();
            HelpModel.HelpDataContext();

            RecvModel = new RecvModel();
            RecvModel.RecvDataContext();

            SendModel = new SendModel();
            SendModel.SendDataContext();

            SerialPortModel = new SerialPortModel();
            SerialPortModel.SerialPortDataContext();

            DepictInfo = "串行端口调试助手";

            TimerModel = new TimerModel();
            TimerModel.TimerDataContext();

            HexSend = false;
            AutoSend = false;
            InitAutoSendTimer();

            SaveRecv = false;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// 受保护的 Dispose 方法实现
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    /* 释放托管资源（如果需要） */
                }

                SPserialPort.Dispose();
                SPserialPort = null;

                disposedValue = true;
            }
        }

        /// <summary>
        /// SerialPort 字段 IDisposable 接口的 Dispose 方法实现（无参数）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
