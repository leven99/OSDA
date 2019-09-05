using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;

namespace OSerialPort.ViewModels
{
    public class MainWindowBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 字段定义
        /// <summary>
        /// 串行端口
        /// </summary>
        public SerialPort SPserialPort = null;
        #endregion

        /// <summary>
        /// 信息描述
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
                if (_DepictInfo != value)
                {
                    _DepictInfo = value;
                    RaisePropertyChanged("DepictInfo");
                }
            }
        }
    }
}
