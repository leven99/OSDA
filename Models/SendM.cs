using OSerialPort.ViewModels;
using System;

namespace OSerialPort.Models
{
    public class SendModel : MainWindowBase
    {
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
                    RaisePropertyChanged(nameof(SendHeader));
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
                    RaisePropertyChanged(nameof(SendDataCount));
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
                    RaisePropertyChanged(nameof(SendData));
                }
            }
        }

        /* 辅助区 - 自送发送的时间间隔 */
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
                    RaisePropertyChanged(nameof(AutoSendNum));
                }
            }
        }

        public void SendDataContext()
        {
            SendData = string.Empty;
            SendDataCount = 0;

            AutoSendNum = 1000;
        }
    }
}
