using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OSDA.ViewModels
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
