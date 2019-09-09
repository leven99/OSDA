using OSerialPort.ViewModels;

namespace OSerialPort.Models
{
    public class HelpModel : MainWindowBase
    {
        public string _VerInfo;
        public string VerInfo
        {
            get
            {
                return _VerInfo;
            }
            set
            {
                _VerInfo = value;
                RaisePropertyChanged(nameof(VerInfo));
            }
        }

        public void HelpDataContext()
        {
            VerInfo = "OSerialPort v2.2.0";
        }
    }
}
