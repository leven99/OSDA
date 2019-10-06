using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace OSDA.ViewModels
{
    public class MainWindowBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 提供区域性信息
        /// </summary>
        internal CultureInfo cultureInfo = new CultureInfo(CultureInfo.CurrentUICulture.Name);

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
