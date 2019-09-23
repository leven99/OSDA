using OSDA.Models;

namespace OSDA.ViewModels
{
    class WPFUpdateViewModel : MainWindowBase
    {
        public HelpModel HelpModel { get; set; }

        public WPFUpdateViewModel()
        {
            HelpModel = new HelpModel();
            HelpModel.HelpDataContext();
        }
    }
}
