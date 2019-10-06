using OSDA.ViewModels;
using System;

namespace OSDA.Models
{
    public class HelpModel : MainWindowBase
    {
        /* json中获取到的标签名称 */
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
        private bool _Reduced_Enable;
        public bool Reduced_Enable
        {
            get
            {
                return _Reduced_Enable;
            }
            set
            {
                if (_Reduced_Enable != value)
                {
                    _Reduced_Enable = value;
                    RaisePropertyChanged(nameof(Reduced_Enable));
                }
            }
        }

        /// <summary>
        /// 视图可见性
        /// </summary>
        private string _View_Visibility;
        public string View_Visibility
        {
            get
            {
                return _View_Visibility;
            }
            set
            {
                if (_View_Visibility != value)
                {
                    _View_Visibility = value;
                    RaisePropertyChanged(nameof(View_Visibility));
                }
            }
        }

        public void HelpDataContext()
        {
            VerInfoNumber = "3.2.0";
            VerInfo = "OSDA v" + VerInfoNumber;

            UpdateInfo = "OSDA发现新版本le........";

            Reduced_Enable = false;
            View_Visibility = "Visible";
        }
    }
}
