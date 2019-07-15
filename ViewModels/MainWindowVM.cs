using System;
using System.Collections.ObjectModel;
using System.IO.Ports;

namespace OSerialPort.ViewModels
{
    class MainWindowVM : MainWindowBase
    {
        private bool IsOpen = false;
        private SerialPort SPserialPort = null;

        /// <summary>
        /// 串口配置
        /// </summary>
        public ObservableCollection<SerialPortVM> SerialPorts { get; set; }

        /// <summary>
        /// 接收区数据
        /// </summary>
        public string ReceData { get; set; }

        /// <summary>
        /// 清空接收区数据
        /// </summary>
        public void ClarReceData()
        {
            ReceData = string.Empty;

        }

        /// <summary>
        /// 发送区数据
        /// </summary>
        public string SendData { get; set; }

        /// <summary>
        /// 清空发送区数据
        /// </summary>
        public void ClearSendData()
        {
            SendData = string.Empty;
        }

        /// <summary>
        /// 接收自动保存（已停止、保存中）
        /// </summary>
        public string AutoSave { get; set; }

        /// <summary>
        /// 接收区数据字节计数
        /// </summary>
        public UInt32 ReceDataCount { get; set; }

        /// <summary>
        /// 发送区数据字节计数
        /// </summary>
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
                _OpenCloseSP = value;
                RaisePropertyChanged(OpenCloseSP);
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
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
                    DepictInfo = "串行端口打开成功";

                    return IsOpen = true;
                }
                else
                {
                    DepictInfo = "串行端口打开失败";

                    return IsOpen = false;
                }
            }
            catch
            {
                DepictInfo = "串行端口发生意外打开失败，请检查线路";

                return IsOpen = false;
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public bool CloseSP()
        {
            try
            {
                if (SPserialPort.IsOpen)
                {
                    SPserialPort.Close();

                    DepictInfo = "串行端口关闭成功";

                    return IsOpen = SPserialPort.IsOpen;
                }
                else
                {
                    DepictInfo = "串行端口已关闭";

                    return IsOpen = SPserialPort.IsOpen;
                }
            }
            catch
            {
                DepictInfo = "串行端口发生意外关闭失败，请检查线路";

                return IsOpen = false;
            }
        }

        /// <summary>
        /// 16进制接收
        /// </summary>
        public bool HexRece { get; set; }

        /// <summary>
        /// 16进制发送
        /// </summary>
        public bool HexSend { get; set; }

        /// <summary>
        /// 自动发送
        /// </summary>
        public bool AutoSend { get; set; }

        /// <summary>
        /// 自动发送间隔时间（单位：ms）
        /// </summary>
        public UInt32 _AutoSendNum;
        public UInt32 AutoSendNum
        {
            get
            {
                return _AutoSendNum;
            }
            set
            {
                _AutoSendNum = value;
                RaisePropertyChanged(AutoSendNum.ToString());
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

        /// <summary>
        /// 多项发送
        /// </summary>
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

        /// <summary>
        /// 保存接收的数据路径选择
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
                    RaisePropertyChanged(_DepictInfo);
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
                    RaisePropertyChanged(SystemTime);
                }
            }
        }

        /// <summary>
        /// 系统时间
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

        public MainWindowVM()
        {
            SerialPorts = new ObservableCollection<SerialPortVM>
            {

            };
            RaisePropertyChanged("SerialPorts");

            OpenCloseSP = "打开串口";

            AutoSendNum = 1000;

            DepictInfo = "串行端口调试助手";
            SystemTime = "System Time";
        }
    }
}
