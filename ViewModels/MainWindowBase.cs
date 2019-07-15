using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OSerialPort.ViewModels
{
    class MainWindowBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
