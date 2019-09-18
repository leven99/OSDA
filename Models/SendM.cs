using OSDA.ViewModels;
using System;

namespace OSDA.Models
{
    public class SendModel : MainWindowBase
    {
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

        /// <summary>
        /// 发送区Header字符串
        /// </summary>
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

        /// <summary>
        /// 辅助区 - 自送发送的时间间隔
        /// </summary>
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
