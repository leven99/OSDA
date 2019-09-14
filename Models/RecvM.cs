using Microsoft.Win32;
using OSDA.Interface;
using OSDA.ViewModels;
using System;
using System.IO;
using System.IO.Ports;

namespace OSDA.Models
{
    public class RecvModel : MainWindowBase
    {
        public string DataRecePath = null;
        /// <summary>
        /// 实现接收区数据超过32MB时，自动清空接收控件中的数据
        /// </summary>
        public int RecvDataDeleteCount = 1;

        public int _RecvDataCount;
        public int RecvDataCount
        {
            get
            {
                return _RecvDataCount;
            }
            set
            {
                if (_RecvDataCount != value)
                {
                    _RecvDataCount = value;
                    RaisePropertyChanged(nameof(RecvDataCount));
                }
            }
        }

        /* 接收区Header中的 [保存中/已停止] 字符串 */
        public string _RecvAutoSave;
        public string RecvAutoSave
        {
            get
            {
                return _RecvAutoSave;
            }
            set
            {
                if (_RecvAutoSave != value)
                {
                    _RecvAutoSave = value;
                    RaisePropertyChanged(nameof(RecvAutoSave));
                }
            }
        }

        public string _RecvHeader;
        public string RecvHeader
        {
            get
            {
                return _RecvHeader;
            }
            set
            {
                if (_RecvHeader != value)
                {
                    _RecvHeader = value;
                    RaisePropertyChanged(nameof(RecvHeader));
                }
            }
        }

        public ITextBoxAppend _RecvData;
        public ITextBoxAppend RecvData
        {
            get
            {
                return _RecvData;
            }
            set
            {
                if (_RecvData != value)
                {
                    _RecvData = value;
                    RaisePropertyChanged(nameof(RecvData));
                }
            }
        }

        /* 辅助区 - 十六进制接收 */
        public bool _HexRecv;
        public bool HexRecv
        {
            get
            {
                return _HexRecv;
            }
            set
            {
                if (_HexRecv != value)
                {
                    _HexRecv = value;
                    RaisePropertyChanged(nameof(HexRecv));
                }
            }
        }

        public void RecvPath()
        {
            SaveFileDialog ReceDataSaveFileDialog = new SaveFileDialog
            {
                Title = "接收数据路径选择",
                FileName = string.Format("{0}", DateTime.Now.ToString("yyyyMMdd")),
                Filter = "文本文件|*.txt"
            };

            if (ReceDataSaveFileDialog.ShowDialog() == true)
            {
                DataRecePath = ReceDataSaveFileDialog.FileName;
            }
        }

        public void RecvDataContext()
        {
            RecvData = new IClassTextBoxAppend();
            RecvDataCount = 0;
            RecvAutoSave = "已停止";
            RecvHeader = "接收区：已接收" + RecvDataCount + "字节，接收自动保存[" + RecvAutoSave + "]";

            HexRecv = false;
        }
    }
}
