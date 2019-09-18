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

        /// <summary>
        /// 接收区Header中的 [保存中/已停止] 字符串
        /// </summary>
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

        /// <summary>
        /// 接收区Header中的 [允许/暂停] 字符串
        /// </summary>
        public string _EnableRecv;
        public string EnableRecv
        {
            get
            {
                return _EnableRecv;
            }
            set
            {
                if (_EnableRecv != value)
                {
                    _EnableRecv = value;
                    RaisePropertyChanged(nameof(EnableRecv));
                }
            }
        }

        /// <summary>
        /// 接收区Header字符串
        /// </summary>
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

        /// <summary>
        /// 接收区 - 允许/暂停接收数据
        /// </summary>
        public bool _Enable_Recv;
        public bool Enable_Recv
        {
            get
            {
                return _Enable_Recv;
            }
            set
            {
                if(_Enable_Recv != value)
                {
                    _Enable_Recv = value;
                    RaisePropertyChanged(nameof(Enable_Recv));
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

        /// <summary>
        /// 辅助区 - 十六进制接收
        /// </summary>
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
            EnableRecv = "（提示：双击文本框更改接收状态）";
            RecvHeader = "接收区：已接收" + RecvDataCount + "字节，接收自动保存[" + RecvAutoSave + "]" + EnableRecv;

            Enable_Recv = true;
            HexRecv = false;
        }
    }
}
