using Microsoft.Win32;
using OSerialPort.Interface;
using OSerialPort.ViewModels;
using System;
using System.IO;
using System.IO.Ports;

namespace OSerialPort.Models
{
    public class RecvModel : MainWindowBase
    {
        #region 字段定义
        public string DataRecePath = null;
        #endregion

        public int _ReceDataCount;
        public int ReceDataCount
        {
            get
            {
                return _ReceDataCount;
            }
            set
            {
                if (_ReceDataCount != value)
                {
                    _ReceDataCount = value;
                    RaisePropertyChanged("ReceDataCount");
                }
            }
        }

        public string _ReceAutoSave;
        public string ReceAutoSave
        {
            get
            {
                return _ReceAutoSave;
            }
            set
            {
                if (_ReceAutoSave != value)
                {
                    _ReceAutoSave = value;
                    RaisePropertyChanged("ReceAutoSave");
                }
            }
        }

        public string _ReceHeader;
        public string ReceHeader
        {
            get
            {
                return _ReceHeader;
            }
            set
            {
                if (_ReceHeader != value)
                {
                    _ReceHeader = value;
                    RaisePropertyChanged("ReceHeader");
                }
            }
        }

        public ITextBoxAppend _ReceData;
        public ITextBoxAppend ReceData
        {
            get
            {
                return _ReceData;
            }
            set
            {
                if (_ReceData != value)
                {
                    _ReceData = value;
                    RaisePropertyChanged("ReceData");
                }
            }
        }

        public bool _HexRece;
        public bool HexRece
        {
            get
            {
                return _HexRece;
            }
            set
            {
                if (_HexRece != value)
                {
                    _HexRece = value;
                    RaisePropertyChanged("HexRece");
                }
            }
        }

        public bool _SaveRece;
        public bool SaveRece
        {
            get
            {
                return _SaveRece;
            }
            set
            {
                if (_SaveRece != value)
                {
                    _SaveRece = value;
                    RaisePropertyChanged("SaveRece");
                }

                if (SaveRece == true)
                {
                    DepictInfo = "接收数据默认保存在程序基目录，可以点击路径选择操作更换";
                }
                else
                {
                    DepictInfo = "串行端口调试助手";
                    ReceAutoSave = "已停止";

                }
            }
        }

        public void RecePath()
        {
            try
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
            catch
            {
                DepictInfo = "路径选择失败";
            }
        }

        public void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort _SerialPort = (SerialPort)sender;

            string recvData = _SerialPort.ReadExisting();

            if (HexRece)
            {
                foreach (char tmp in recvData.ToCharArray())
                {
                    ReceData.Append(string.Format("{0:X2} ", Convert.ToInt32(tmp)));
                }
            }
            else
            {
                ReceData.Append(recvData);
            }

            if (SaveRece)
            {
                ReceAutoSave = "保存中";

                SaveReceData(recvData);
            }
            else
            {
                ReceAutoSave = "已停止";
            }

            ReceDataCount += recvData.Length;

            ReceHeader = "接收区：已接收" + ReceDataCount + "字节，接收自动保存[" + ReceAutoSave + "]";
        }

        public void SaveReceData(string ReceData)
        {
            try
            {
                if (DataRecePath == null)
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\ReceData\\");

                    using (StreamWriter DefaultReceDataPath = new StreamWriter(
                        AppDomain.CurrentDomain.BaseDirectory + "\\ReceData\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt",
                        true))
                    {
                        DefaultReceDataPath.WriteAsync(ReceData);
                    }
                }
                else
                {
                    using (StreamWriter DefaultReceDataPath = new StreamWriter(DataRecePath, true))
                    {
                        DefaultReceDataPath.WriteAsync(ReceData);
                    }
                }
            }
            catch
            {
                DepictInfo = "接收数据保存失败";
            }
        }

        public void RecvDataContext()
        {
            ReceData = new IClassTextBoxAppend();
            ReceDataCount = 0;
            ReceAutoSave = "已停止";
            ReceHeader = "接收区：已接收" + ReceDataCount + "字节，接收自动保存[" + ReceAutoSave + "]";

            HexRece = false;
            SaveRece = false;
        }
    }
}
