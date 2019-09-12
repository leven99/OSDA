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
    }
}
