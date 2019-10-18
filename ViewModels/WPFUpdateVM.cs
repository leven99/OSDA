using OSDA.Models;

namespace OSDA.ViewModels
{
    internal class WPFUpdateViewModel : MainWindowBase
    {
        public HelpModel HelpModel { get; set; }

        public WPFUpdateViewModel()
        {
            HelpModel = new HelpModel();
            HelpModel.HelpDataContext();
        }
    }
}
