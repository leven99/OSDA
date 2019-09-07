using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;

namespace OSerialPort.ViewModels
{
    public class MainWindowBase : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
