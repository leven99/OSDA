using OSDA.ViewModels;
using System;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace OSDA.Models
{
    public class HelpModel : MainWindowBase, IDisposable
    {
        public string _VerInfoNumber;
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

        public string _VerInfo;
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

        public string _UpdateVerInfoNumber;
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

        /* 可以获取json内容的两个地址 */
        public readonly Uri gitee_uri = new Uri("https://gitee.com/api/v5/repos/leven9/OSDA/releases/latest");
        public readonly Uri github_cri = new Uri("https://api.github.com/repos/leven99/OSDA/releases/latest");

        /* json中获取到的标签名称 */
        public struct UpdateJson
        {
            public string Tag_name { get; set; }
        }

        public UpdateJson updateJson;

        public HttpClient httpClient = null;
        public readonly JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

        public void HelpDataContext()
        {
            VerInfoNumber = "3.0.0";

            VerInfo = "OSDA v" + VerInfoNumber;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// 受保护的 Dispose 方法实现
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    /* 释放托管资源（如果需要） */
                }

                httpClient.Dispose();
                httpClient = null;

                disposedValue = true;
            }
        }

        /// <summary>
        /// httpClient 字段 IDisposable 接口的 Dispose 方法实现（无参数）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
