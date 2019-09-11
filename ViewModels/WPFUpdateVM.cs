using OSerialPort.Models;

namespace OSerialPort.ViewModels
{
    class WPFUpdateViewModel : MainWindowBase
    {
        public string _UpdateInfo;
        public string UpdateInfo
        {
            get
            {
                return _UpdateInfo;
            }
            set
            {
                if(_UpdateInfo != value)
                {
                    _UpdateInfo = value;
                    RaisePropertyChanged(nameof(UpdateInfo));
                }
            }
        }

        public WPFUpdateViewModel()
        {
            UpdateInfo = "OSerialPort发现新版本le........";
        }
    }
}
