using OSDA.ViewModels;

namespace OSDA.Models
{
    public class HelpModel : MainWindowBase
    {
        /// <summary>
        /// 从服务器获取到的Json结构数据
        /// </summary>
        public struct UpdateJson
        {
            public string Tag_name { get; set; }
        }

        /// <summary>
        /// 本地软件的版本号
        /// </summary>
        private string _VerInfoNumber;
        public string VerInfoNumber
        {
            get
            {
                return _VerInfoNumber;
            }
            set
            {
                if (_VerInfoNumber != value)
                {
                    _VerInfoNumber = value;
                    RaisePropertyChanged(nameof(VerInfoNumber));
                }
            }
        }

        /// <summary>
        /// 本地软件的版本信息
        /// </summary>
        private string _VerInfo;
        public string VerInfo
        {
            get
            {
                return _VerInfo;
            }
            set
            {
                if(_VerInfo != value)
                {
                    _VerInfo = value;
                    RaisePropertyChanged(nameof(VerInfo));
                }
            }
        }

        /// <summary>
        /// 服务器软件的版本号
        /// </summary>
        private string _UpdateVerInfoNumber;
        public string UpdateVerInfoNumber
        {
            get
            {
                return _UpdateVerInfoNumber;
            }
            set
            {
                if(_UpdateVerInfoNumber != value)
                {
                    _UpdateVerInfoNumber = value;
                    RaisePropertyChanged(nameof(UpdateVerInfoNumber));
                }
            }
        }

        /// <summary>
        /// 服务器软件的版本信息
        /// </summary>
        private string _UpdateInfo;
        public string UpdateInfo
        {
            get
            {
                return _UpdateInfo;
            }
            set
            {
                if (_UpdateInfo != value)
                {
                    _UpdateInfo = value;
                    RaisePropertyChanged(nameof(UpdateInfo));
                }
            }
        }

        /// <summary>
        /// 精简视图
        /// </summary>
        private bool _ReducedEnable;
        public bool ReducedEnable
        {
            get
            {
                return _ReducedEnable;
            }
            set
            {
                if (_ReducedEnable != value)
                {
                    _ReducedEnable = value;
                    RaisePropertyChanged(nameof(ReducedEnable));
                }
            }
        }

        /// <summary>
        /// 视图可见性
        /// </summary>
        private string _ViewVisibility;
        public string ViewVisibility
        {
            get
            {
                return _ViewVisibility;
            }
            set
            {
                if (_ViewVisibility != value)
                {
                    _ViewVisibility = value;
                    RaisePropertyChanged(nameof(ViewVisibility));
                }
            }
        }

        public void HelpDataContext()
        {
            VerInfoNumber = "3.2.0";
            VerInfo = "OSDA v" + VerInfoNumber;

            UpdateInfo = string.Format(cultureInfo, "OSDA发现新版本le........");

            ReducedEnable = false;
            ViewVisibility = "Visible";
        }
    }
}
