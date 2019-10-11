using Microsoft.Win32;
using OSDA.Models;
using OSDA.Views;
using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace OSDA.ViewModels
{
    class MainWindowViewModel : MainWindowBase, IDisposable
    {
        #region 字段
        public SerialPort SPserialPort = new SerialPort();

        private readonly Uri gitee_uri = new Uri("https://gitee.com/api/v5/repos/leven9/OSDA/releases/latest");
        private readonly Uri github_cri = new Uri("https://api.github.com/repos/leven99/OSDA/releases/latest");

        private HttpClient httpClient = new HttpClient();

        private readonly DataContractJsonSerializer ReleaseDeserializer = new DataContractJsonSerializer(typeof(GitRelease));

        private string DataRecvPath = null;   /* 数据接收路径 */

        /// <summary>
        /// 用于接收区数据超过32MB时，自动清空接收控件中的数据
        /// </summary>
        private volatile Int32 RecvDataDeleteCount = 1;
        #endregion

        public SerialPortModel SerialPortModel { get; set; }
        public SendModel SendModel { get; set; }
        public RecvModel RecvModel { get; set; }
        public TimerModel TimerModel { get; set; }
        public HelpModel HelpModel { get; set; }
        public GitRelease LatestRelease { get; set; }

        #region 状态栏- 信息描述
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
        #endregion

        #region 菜单栏

        #region 选项

        #region 字节编码
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
                DepictInfo = string.Format(cultureInfo, "更改字节编码为{0}是非法操作", e.ParamName);
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
                DepictInfo = string.Format(cultureInfo, "更改字节编码为{0}是非法操作", e.ParamName);
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
                DepictInfo = string.Format(cultureInfo, "设置字节编码为{0}是非法操作", e.ParamName);
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
                DepictInfo = string.Format(cultureInfo, "更改字节编码为{0}是非法操作", e.ParamName);
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
                DepictInfo = string.Format(cultureInfo, "[{0}]当设置为硬件流或硬软件流时，不允许设置RTS",
                    e.HResult.ToString("X", cultureInfo));
            }
            catch (IOException e)
            {
                DepictInfo = string.Format(cultureInfo, "[{0}]端口处于无效状态", e.HResult.ToString("X", cultureInfo));
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
                DepictInfo = string.Format(cultureInfo, "[{0}]当设置为硬件流或硬软件流时，不允许设置DTR",
                    e.HResult.ToString("X", cultureInfo));
            }
            catch (IOException e)
            {
                DepictInfo = string.Format(cultureInfo, "[{0}]端口处于无效状态", e.HResult.ToString("X", cultureInfo));
            }
        }

        #region 流控制
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
                DepictInfo = string.Format(cultureInfo, "[{0}]端口处于无效状态", e.HResult.ToString("X", cultureInfo));
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format(cultureInfo, "设置流控制为{0}是非法操作", e.ParamName);
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
                DepictInfo = string.Format(cultureInfo, "[{0}]端口处于无效状态", e.HResult.ToString("X", cultureInfo));
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format(cultureInfo, "设置流控制为{0}是非法操作", e.ParamName);
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
                DepictInfo = string.Format(cultureInfo, "[{0}]端口处于无效状态", e.HResult.ToString("X", cultureInfo));
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format(cultureInfo, "设置流控制为{0}是非法操作", e.ParamName);
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
                DepictInfo = string.Format(cultureInfo, "[{0}]端口处于无效状态", e.HResult.ToString("X", cultureInfo));
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format(cultureInfo, "设置流控制为{0}是非法操作", e.ParamName);
            }
        }
        #endregion

        #region 发送换行
        public void NonesEnable()
        {
            SendModel.CrEnable = false;
            SendModel.LfEnable = false;
            SendModel.CrLfEnable = false;

            SendModel.NonesEnable = true;
        }

        public void CrEnable()
        {
            SendModel.NonesEnable = false;
            SendModel.LfEnable = false;
            SendModel.CrLfEnable = false;

            SendModel.CrEnable = true;
        }

        public void  LfEnable()
        {
            SendModel.NonesEnable = false;
            SendModel.CrEnable = false;
            SendModel.CrLfEnable = false;

            SendModel.LfEnable = true;
        }

        public void CrLfEnable()
        {
            SendModel.NonesEnable = false;
            SendModel.CrEnable = false;
            SendModel.LfEnable = false;

            SendModel.CrLfEnable = true;
        }
        #endregion

        #endregion

        #region 视图
        public void Reduced_Enable()
        {
            HelpModel.ReducedEnable = !HelpModel.ReducedEnable;

            if(HelpModel.ReducedEnable)
            {
                HelpModel.ViewVisibility = "Collapsed";
            }
            else
            {
                HelpModel.ViewVisibility = "Visible";
            }
        }
        #endregion

        #region 帮助
        public async void UpdateAsync()
        {
            LatestRelease = await DownloadJsonObjectAsync<GitRelease>(github_cri, ReleaseDeserializer, "github")
                .ConfigureAwait(false);

            if(LatestRelease == default)
            {
                DepictInfo = string.Format(cultureInfo, "GitHub.com 服务器请求/响应异常，更换服务器......请稍后");

                LatestRelease = await DownloadJsonObjectAsync<GitRelease>(gitee_uri, ReleaseDeserializer, "gitee")
                    .ConfigureAwait(false);

                if (LatestRelease == default)
                {
                    DepictInfo = string.Format(cultureInfo, "服务器异常，请检查网络或稍后再试！");

                    return;
                }
            }

            UpdateVersionCompareTo(LatestRelease.GetVersion());
        }

        protected async Task<T> DownloadJsonObjectAsync<T>(Uri address, DataContractJsonSerializer serializer, string git)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                httpClient.Timeout = TimeSpan.FromMilliseconds(3000);

                if (git == "github")
                {
                    DepictInfo = string.Format(cultureInfo, "正在向 GitHub.com 请求数据......");
                }
                else if (git == "gitee")
                {
                    DepictInfo = string.Format(cultureInfo, "正在向 Gitee.com 请求数据......");
                }

                var _resPonse = await httpClient.GetAsync(address).ConfigureAwait(false);

                if (git == "github")
                {
                    DepictInfo = string.Format(cultureInfo, "等待 GitHub.com 响应数据......");
                }
                else if (git == "gitee")
                {
                    DepictInfo = string.Format(cultureInfo, "等待 Gitee.com 响应数据......");
                }

                var _jsonData = await _resPonse.Content.ReadAsStringAsync().ConfigureAwait(false);

                using (var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(_jsonData)))
                {
                    return (T)serializer.ReadObject(jsonStream);
                }
            }
            catch
            {
                return default;
                throw;
            }
        }

        private void UpdateVersionCompareTo(Version NewVersion)
        {
            Version OldVersion = new Version(HelpModel.VerInfoNumber);

            if (NewVersion.CompareTo(OldVersion) > 0)
            {
                Thread wPFUpdateThread = new Thread(new ThreadStart(ThreadStartingWPFUpdate));
                wPFUpdateThread.SetApartmentState(ApartmentState.STA);
                wPFUpdateThread.IsBackground = true;
                wPFUpdateThread.Start();
            }
            else
            {
                DepictInfo = string.Format(cultureInfo, "OSDA v" + HelpModel.VerInfoNumber + " 已经是最新版le......");
            }
        }

        private void ThreadStartingWPFUpdate()
        {
            DepictInfo = string.Format(cultureInfo, "串行端口调试助手");

            WPFUpdate wPFUpdate = new WPFUpdate();
            wPFUpdate.Show();
            Dispatcher.Run();
        }
        #endregion

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
                SPserialPort.StopBits = SerialPortModel.GetStopBits(SerialPortModel.SPStopBits.ToString(cultureInfo));
                SPserialPort.Parity = SerialPortModel.GetParity(SerialPortModel.SPParity.ToString(cultureInfo));

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
                    SerialPortModel.OpenCloseSP = string.Format(cultureInfo, "关闭串口");
                    DepictInfo = string.Format(cultureInfo, "成功打开串行端口{0}、波特率{1}、数据位{2}、停止位{3}、校验位{4}",
                        SPserialPort.PortName, SPserialPort.BaudRate.ToString(cultureInfo), SPserialPort.DataBits.ToString(cultureInfo),
                        SPserialPort.StopBits.ToString(), SPserialPort.Parity.ToString());

                    SerialPortModel.SPPortEnable = false;
                    SerialPortModel.SPBaudRateEnable = false;
                    SerialPortModel.SPDataBitsEnable = false;
                    SerialPortModel.SPStopBitsEnable = false;
                    SerialPortModel.SPParityEnable = false;

                    if (RecvModel.EnableRecv)
                    {
                        RecvModel.RecvEnable = string.Format(cultureInfo, "允许");
                    }
                    else
                    {
                        RecvModel.RecvEnable = string.Format(cultureInfo, "暂停");
                    }
                    RecvModel.RecvHeader = string.Format(cultureInfo, "接收区：已接收" + RecvModel.RecvDataCount +
                        "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]，接收状态[" + RecvModel.RecvEnable + "]");

                    return true;
                }
                else
                {
                    DepictInfo = string.Format(cultureInfo, "串行端口打开失败");

                    return false;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                DepictInfo = string.Format(cultureInfo, "[{0}]端口访问被拒绝", e.HResult.ToString("X", cultureInfo));

                return false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = string.Format(cultureInfo, "串口属性{0}是非法的", e.ParamName);

                return false;
            }
            catch (ArgumentException e)
            {
                DepictInfo = string.Format(cultureInfo, "串口{0}不支持", e.ParamName);

                return false;
            }
            catch (IOException e)
            {
                DepictInfo = string.Format(cultureInfo, "[{0}]端口处于无效状态", e.HResult.ToString("X", cultureInfo));

                return false;
            }
            catch (InvalidOperationException e)
            {
                DepictInfo = string.Format(cultureInfo, "[{0}]指定端口已经打开", e.HResult.ToString("X", cultureInfo));

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
                    SerialPortModel.OpenCloseSP = string.Format(cultureInfo, "打开串口");

                    DepictInfo = string.Format(cultureInfo, "串行端口关闭成功");

                    SerialPortModel.SPPortEnable = true;
                    SerialPortModel.SPBaudRateEnable = true;
                    SerialPortModel.SPDataBitsEnable = true;
                    SerialPortModel.SPStopBitsEnable = true;
                    SerialPortModel.SPParityEnable = true;

                    RecvModel.RecvAutoSave = string.Format(cultureInfo, "已停止");
                    RecvModel.RecvHeader = string.Format(cultureInfo, "接收区：已接收" + RecvModel.RecvDataCount +
                            "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]，接收状态[" + RecvModel.RecvEnable + "]");

                    return SPserialPort.IsOpen;
                }
                else
                {
                    DepictInfo = string.Format(cultureInfo, "串行端口已关闭");

                    return SPserialPort.IsOpen;
                }
            }
            catch(IOException e)
            {
                DepictInfo = string.Format(cultureInfo, "[{0}]端口处于无效状态", e.HResult.ToString("X", cultureInfo));

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
                        DepictInfo = string.Format(cultureInfo, "请输入十六进制数据用空格隔开，比如A0 B1 C2 D3");
                    }
                    else
                    {
                        DepictInfo = string.Format(cultureInfo, "串行端口调试助手");
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
                            DepictInfo = string.Format(cultureInfo, "请输入正确的发送时间间隔");
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
                if ((SPserialPort == null) && (SPserialPort.IsOpen == false))
                {
                    return;
                }

                Int32 SendCount = 0;

                if (HexSend)
                {
                    string[] _sendData = SendModel.SendData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    byte[] sendData = new byte[_sendData.Length];

                    foreach (var tmp in _sendData)
                    {
                        sendData[SendCount++] = byte.Parse(tmp, NumberStyles.AllowHexSpecifier, cultureInfo);
                    }

                    SPserialPort.Write(sendData, 0, SendCount);

                }
                else
                {
                    SendCount = SPserialPort.Encoding.GetByteCount(SendModel.SendData);
                    SPserialPort.Write(SPserialPort.Encoding.GetBytes(SendModel.SendData), 0, SendCount);
                }

                if (SendModel.NonesEnable)
                {
                    SendModel.SendDataCount += SendCount;
                }
                else if (SendModel.CrEnable)
                {
                    SPserialPort.Write(SPserialPort.Encoding.GetBytes("\r"), 0, 1);
                    SendModel.SendDataCount += (SendCount + 1);
                }
                else if (SendModel.LfEnable)
                {
                    SPserialPort.Write(SPserialPort.Encoding.GetBytes("\n"), 0, 1);
                    SendModel.SendDataCount += (SendCount + 1);
                }
                else if (SendModel.CrLfEnable)
                {
                    SPserialPort.Write(SPserialPort.Encoding.GetBytes("\r\n"), 0, 2);
                    SendModel.SendDataCount += (SendCount + 2);
                }
            }
            catch
            {
                DepictInfo = string.Format(cultureInfo, "发送异常，请检查发送数据");
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
            SaveFileDialog ReceDataSaveFileDialog = new SaveFileDialog
            {
                Title = string.Format(cultureInfo, "接收数据路径选择"),
                FileName = string.Format(cultureInfo, "{0}", DateTime.Now.ToString("yyyyMMdd", cultureInfo)),
                Filter = string.Format(cultureInfo, "文本文件|*.txt")
            };

            if (ReceDataSaveFileDialog.ShowDialog() == true)
            {
                DataRecvPath = ReceDataSaveFileDialog.FileName;
            }
        }
        #endregion

        #region 清接收区
        public void ClarReceData()
        {
            RecvModel.RecvData.Delete();

            RecvDataDeleteCount = 1;
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
            RecvModel.RecvHeader = string.Format(cultureInfo, "接收区：已接收" + RecvModel.RecvDataCount +
                "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]，接收状态[" + RecvModel.RecvEnable + "]");

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

            if (RecvModel.EnableRecv)
            {
                if (RecvModel.HexRecv)
                {
                    foreach (var tmp in recvData)
                    {
                        RecvModel.RecvData.Append(string.Format(cultureInfo, "{0:X2} ", tmp));
                    }
                }
                else
                {
                    RecvModel.RecvData.Append(_SerialPort.Encoding.GetString(recvData));
                }
            }

            if (SaveRecv)
            {
                RecvModel.RecvAutoSave = string.Format(cultureInfo, "保存中");

                SaveRecvData(_SerialPort.Encoding.GetString(recvData));
            }
            else
            {
                RecvModel.RecvAutoSave = string.Format(cultureInfo, "已停止");
            }

            RecvModel.RecvDataCount += recvData.Length;

            RecvModel.RecvHeader = string.Format(cultureInfo, "接收区：已接收" + RecvModel.RecvDataCount +
                "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]，接收状态[" + RecvModel.RecvEnable + "]");

            if (RecvModel.RecvDataCount > (32768 * RecvDataDeleteCount))
            {
                RecvModel.RecvData.Delete();

                RecvDataDeleteCount += 1;   /* 接收区数据达到32MB（32768KB）或其倍数，则自动清空接收区数据 */
            }
        }

        public async void SaveRecvData(string ReceData)
        {
            try
            {
                if (DataRecvPath == null)
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\ReceData\\");

                    using (StreamWriter DefaultReceDataPath = new StreamWriter(
                        AppDomain.CurrentDomain.BaseDirectory +
                        "\\ReceData\\" + DateTime.Now.ToString("yyyyMMdd", cultureInfo) + ".txt",
                        true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData).ConfigureAwait(false);
                    }
                }
                else
                {
                    using (StreamWriter DefaultReceDataPath = new StreamWriter(DataRecvPath, true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData).ConfigureAwait(false);
                    }
                }
            }
            catch
            {
                RecvModel.RecvAutoSave = string.Format(cultureInfo, "已停止");
                RecvModel.RecvHeader = string.Format(cultureInfo, "接收区：已接收" + RecvModel.RecvDataCount + 
                    "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]，接收状态[" + RecvModel.RecvEnable + "]");

                DepictInfo = string.Format(cultureInfo, "接收数据保存失败");
            }
        }
        #endregion

        #region RecvTextBox Mouse Double Support
        public void Enable_Recv()
        {
            RecvModel.EnableRecv = !RecvModel.EnableRecv;

            if(RecvModel.EnableRecv)
            {
                RecvModel.RecvEnable = "允许";
            }
            else
            {
                RecvModel.RecvEnable = "暂停";
            }

            RecvModel.RecvHeader = "接收区：已接收" + RecvModel.RecvDataCount +
                "字节，接收自动保存[" + RecvModel.RecvAutoSave + "]，接收状态[" + RecvModel.RecvEnable + "]";
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

            DepictInfo = string.Format(cultureInfo, "串行端口调试助手");

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

                httpClient.Dispose();
                httpClient = null;

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
