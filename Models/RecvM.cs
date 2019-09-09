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
        public string DataRecePath = null;
        /// <summary>
        /// 接收区自动清空计数
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
                    RaisePropertyChanged("RecvDataCount");
                }
            }
        }

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
                    RaisePropertyChanged("RecvAutoSave");
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
                    RaisePropertyChanged("RecvHeader");
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
                    RaisePropertyChanged("RecvData");
                }
            }
        }

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
                    RaisePropertyChanged("HexRecv");
                }
            }
        }

        public bool _SaveRecv;
        public bool SaveRecv
        {
            get
            {
                return _SaveRecv;
            }
            set
            {
                if (_SaveRecv != value)
                {
                    _SaveRecv = value;
                    RaisePropertyChanged("SaveRecv");
                }

                if (SaveRecv == true)
                {
                    DepictInfo = "接收数据默认保存在程序基目录，可以点击路径选择操作更换";
                }
                else
                {
                    DepictInfo = "串行端口调试助手";
                    RecvAutoSave = "已停止";

                }
            }
        }

        public void RecvPath()
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

            if (HexRecv)
            {
                foreach (char tmp in recvData.ToCharArray())
                {
                    RecvData.Append(string.Format("{0:X2} ", Convert.ToInt32(tmp)));
                }
            }
            else
            {
                RecvData.Append(recvData);
            }

            if (SaveRecv)
            {
                RecvAutoSave = "保存中";

                SaveRecvData(recvData);
            }
            else
            {
                RecvAutoSave = "已停止";
            }

            RecvDataCount += recvData.Length;

            RecvHeader = "接收区：已接收" + RecvDataCount + "字节，接收自动保存[" + RecvAutoSave + "]";

            if(RecvDataCount > (32768 * RecvDataDeleteCount))
            {
                RecvData.Delete();   /* 32MB */

                RecvDataDeleteCount += 1;
            }
        }

        public async void SaveRecvData(string ReceData)
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
                        await DefaultReceDataPath.WriteAsync(ReceData);
                    }
                }
                else
                {
                    using (StreamWriter DefaultReceDataPath = new StreamWriter(DataRecePath, true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData);
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
            RecvData = new IClassTextBoxAppend();
            RecvDataCount = 0;
            RecvAutoSave = "已停止";
            RecvHeader = "接收区：已接收" + RecvDataCount + "字节，接收自动保存[" + RecvAutoSave + "]";

            HexRecv = false;
            SaveRecv = false;
        }
    }
}
