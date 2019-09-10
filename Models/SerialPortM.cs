using OSerialPort.ViewModels;
using System.IO.Ports;
using System.Windows.Media;

namespace OSerialPort.Models
{
    class SerialPortModel : MainWindowBase
    {
        public string[] LSPPort { get; set; }
        public int[] LSPBaudRate { get; set; }
        public int[] LSPDataBits { get; set; }
        public string[] LSPStopBits { get; set; }
        public string[] LSPParity { get; set; }

        #region 串口属性
        public string _SPPort;
        public string SPPort
        {
            get
            {
                return _SPPort;
            }
            set
            {
                if (_SPPort != value)
                {
                    _SPPort = value;
                    RaisePropertyChanged(nameof(SPPort));
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
                if (_SPBaudRate != value)
                {
                    _SPBaudRate = value;
                    RaisePropertyChanged(nameof(SPBaudRate));
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
                if (_SPDataBits != value)
                {
                    _SPDataBits = value;
                    RaisePropertyChanged(nameof(SPDataBits));
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
                if (_SPStopBits != value)
                {
                    _SPStopBits = value;
                    RaisePropertyChanged(nameof(SPStopBits));
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
                if (_SPParity != value)
                {
                    _SPParity = value;
                    RaisePropertyChanged(nameof(SPParity));
                }
            }
        }
        #endregion

        public Brush _SPBrush;
        public Brush SPBrush
        {
            get
            {
                return _SPBrush;
            }
            set
            {
                if (_SPBrush != value)
                {
                    _SPBrush = value;
                    RaisePropertyChanged(nameof(SPBrush));
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
                    RaisePropertyChanged(nameof(OpenCloseSP));
                }
            }
        }

        #region 串口属性控件[启用/不启用]，用于实现串口打开时不允许更改串口属性
        public bool _SPPortEnable;
        public bool SPPortEnable
        {
            get
            {
                return _SPPortEnable;
            }
            set
            {
                if (_SPPortEnable != value)
                {
                    _SPPortEnable = value;
                    RaisePropertyChanged(nameof(SPPortEnable));
                }
            }
        }

        public bool _SPBaudRateEnable;
        public bool SPBaudRateEnable
        {
            get
            {
                return _SPBaudRateEnable;
            }
            set
            {
                if (_SPBaudRateEnable != value)
                {
                    _SPBaudRateEnable = value;
                    RaisePropertyChanged(nameof(SPBaudRateEnable));
                }
            }
        }

        public bool _SPDataBitsEnable;
        public bool SPDataBitsEnable
        {
            get
            {
                return _SPDataBitsEnable;
            }
            set
            {
                if (_SPDataBitsEnable != value)
                {
                    _SPDataBitsEnable = value;
                    RaisePropertyChanged(nameof(SPDataBitsEnable));
                }
            }
        }

        public bool _SPStopBitsEnable;
        public bool SPStopBitsEnable
        {
            get
            {
                return _SPStopBitsEnable;
            }
            set
            {
                if (_SPStopBitsEnable != value)
                {
                    _SPStopBitsEnable = value;
                    RaisePropertyChanged(nameof(SPStopBitsEnable));
                }
            }
        }

        public bool _SPParityEnable;
        public bool SPParityEnable
        {
            get
            {
                return _SPParityEnable;
            }
            set
            {
                if (_SPParityEnable != value)
                {
                    _SPParityEnable = value;
                    RaisePropertyChanged(nameof(SPParityEnable));
                }
            }
        }
        #endregion

        #region 字节编码
        public bool _ASCIIEnable;
        public bool ASCIIEnable
        {
            get
            {
                return _ASCIIEnable;
            }
            set
            {
                if(_ASCIIEnable != value)
                {
                    _ASCIIEnable = value;
                    RaisePropertyChanged(nameof(ASCIIEnable));
                }
            }
        }

        public bool _UTF8Enable;
        public bool UTF8Enable
        {
            get
            {
                return _UTF8Enable;
            }
            set
            {
                if (_UTF8Enable != value)
                {
                    _UTF8Enable = value;
                    RaisePropertyChanged(nameof(UTF8Enable));
                }
            }
        }

        public bool _UTF16Enable;
        public bool UTF16Enable
        {
            get
            {
                return _UTF16Enable;
            }
            set
            {
                if (_UTF16Enable != value)
                {
                    _UTF16Enable = value;
                    RaisePropertyChanged(nameof(UTF16Enable));
                }
            }
        }

        public bool _UTF32Enable;
        public bool UTF32Enable
        {
            get
            {
                return _UTF32Enable;
            }
            set
            {
                if (_UTF32Enable != value)
                {
                    _UTF32Enable = value;
                    RaisePropertyChanged(nameof(UTF32Enable));
                }
            }
        }
        #endregion

        public bool _DtrEnable;
        public bool DtrEnable
        {
            get
            {
                return _DtrEnable;
            }
            set
            {
                if (_DtrEnable != value)
                {
                    _DtrEnable = value;
                    RaisePropertyChanged(nameof(DtrEnable));
                }
            }
        }

        public bool _RtsEnable;
        public bool RtsEnable
        {
            get
            {
                return _RtsEnable;
            }
            set
            {
                if (_RtsEnable != value)
                {
                    _RtsEnable = value;
                    RaisePropertyChanged(nameof(RtsEnable));
                }
            }
        }

        #region 流控制
        public bool _NoneEnable;
        public bool NoneEnable
        {
            get
            {
                return _NoneEnable;
            }
            set
            {
                if(_NoneEnable != value)
                {
                    _NoneEnable = value;
                    RaisePropertyChanged(nameof(NoneEnable));
                }
            }
        }

        public bool _RequestToSendEnable;
        public bool RequestToSendEnable
        {
            get
            {
                return _RequestToSendEnable;
            }
            set
            {
                if (_RequestToSendEnable != value)
                {
                    _RequestToSendEnable = value;
                    RaisePropertyChanged(nameof(RequestToSendEnable));
                }
            }
        }

        public bool _XOnXOffEnable;
        public bool XOnXOffEnable
        {
            get
            {
                return _XOnXOffEnable;
            }
            set
            {
                if (_XOnXOffEnable != value)
                {
                    _XOnXOffEnable = value;
                    RaisePropertyChanged(nameof(XOnXOffEnable));
                }
            }
        }

        public bool _RequestToSendXOnXOffEnable;
        public bool RequestToSendXOnXOffEnable
        {
            get
            {
                return _RequestToSendXOnXOffEnable;
            }
            set
            {
                if (_RequestToSendXOnXOffEnable != value)
                {
                    _RequestToSendXOnXOffEnable = value;
                    RaisePropertyChanged(nameof(RequestToSendXOnXOffEnable));
                }
            }
        }
        #endregion

        #region 信号状态
        public Brush _DcdBrush;
        public Brush DcdBrush
        {
            get
            {
                return _DcdBrush;
            }
            set
            {
                if (_DcdBrush != value)
                {
                    _DcdBrush = value;
                    RaisePropertyChanged(nameof(DcdBrush));
                }
            }
        }

        public Brush _CtsBrush;
        public Brush CtsBrush
        {
            get
            {
                return _CtsBrush;
            }
            set
            {
                if (_CtsBrush != value)
                {
                    _CtsBrush = value;
                    RaisePropertyChanged(nameof(CtsBrush));
                }
            }
        }

        public Brush _DsrBrush;
        public Brush DsrBrush
        {
            get
            {
                return _DsrBrush;
            }
            set
            {
                if (_DsrBrush != value)
                {
                    _DsrBrush = value;
                    RaisePropertyChanged(nameof(DsrBrush));
                }
            }
        }
        #endregion

        public void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SerialPort _SerialPort = (SerialPort)sender;

            switch (e.EventType)
            {
                case SerialPinChange.CDChanged:
                    if (_SerialPort.CDHolding)
                    {
                        DcdBrush = Brushes.GreenYellow;
                    }
                    else
                    {
                        DcdBrush = Brushes.Black;
                    }
                    break;
                case SerialPinChange.CtsChanged:
                    if (_SerialPort.CtsHolding)
                    {
                        CtsBrush = Brushes.GreenYellow;
                    }
                    else
                    {
                        CtsBrush = Brushes.Black;
                    }
                    break;
                case SerialPinChange.DsrChanged:
                    if (_SerialPort.DsrHolding)
                    {
                        DsrBrush = Brushes.GreenYellow;
                    }
                    else
                    {
                        DsrBrush = Brushes.Black;
                    }
                    break;
                default:
                    break;
            }
        }

        #region 计算停止位和校验位
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

        public void SerialPortDataContext()
        {
            LSPPort = SerialPort.GetPortNames();
            LSPBaudRate = new int[] { 1200, 2400, 4800, 7200, 9600, 14400, 19200, 38400, 57600, 115200, 128000, 230400 };
            LSPDataBits = new int[] { 5, 6, 7, 8 };
            LSPStopBits = new string[] { "One", "Two", "OnePointFive" };
            LSPParity = new string[] { "None", "Odd", "Even", "Mark", "Space" };

            SPBaudRate = 9600;
            SPDataBits = 8;
            SPStopBits = "One";
            SPParity = "None";

            SPBrush = Brushes.Red;
            OpenCloseSP = "打开串口";

            /* 串口属性控件 */
            SPPortEnable = true;
            SPBaudRateEnable = true;
            SPDataBitsEnable = true;
            SPStopBitsEnable = true;
            SPParityEnable = true;

            /* 字节编码 */
            ASCIIEnable = false;
            UTF8Enable = true;
            UTF16Enable = false;
            UTF32Enable = false;

            DtrEnable = false;
            RtsEnable = false;

            /* 流控制 */
            NoneEnable = true;
            RequestToSendEnable = false;
            XOnXOffEnable = false;
            RequestToSendXOnXOffEnable = false;

            /* 信号状态 */
            DcdBrush = Brushes.Black;
            CtsBrush = Brushes.Black;
            DsrBrush = Brushes.Black;
        }
    }
}
