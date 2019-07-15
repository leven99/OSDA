using System;
using System.IO.Ports;

namespace OSerialPort.ViewModels
{
    class MainWindow
    {
        #region 字段
        private bool IsOpen = false;
        private SerialPort SPserialPort = null;
        #endregion

        /// <summary>
        /// 接收区数据
        /// </summary>
        public string ReceData { get; set; }

        /// <summary>
        /// 发送区数据
        /// </summary>
        public string SendData { get; set; }

        /// <summary>
        /// 接收自动保存
        /// </summary>
        public string AutoSave { get; set; }

        /// <summary>
        /// 接收区数据计数
        /// </summary>
        public UInt32 ReceDataCount { get; set; }

        /// <summary>
        /// 发送区数据计数
        /// </summary>
        public UInt32 SendDataCount { get; set; }

        /// <summary>
        /// 16进制接收
        /// </summary>
        public bool HexRece { get; set; }

        /// <summary>
        /// 16进制发送
        /// </summary>
        public bool HexSend { get; set; }

        /// <summary>
        /// 自动发送
        /// </summary>
        public bool AutoSend { get; set; }

        /// <summary>
        /// 自动发送间隔时间（单位：ms）
        /// </summary>
        public UInt32 AutoSendNum { get; set; }

        /// <summary>
        /// 保存接收
        /// </summary>
        public bool SaveRece { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string DepictInfo { get; set; }

        /// <summary>
        /// 系统时间
        /// </summary>
        public string SystemTime { get; set; }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public bool OpenSP()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                return CloseSP();
            }

            try
            {
                if (SPserialPort.IsOpen)
                {
                    DepictInfo = "串行端口打开成功";

                    return IsOpen = true;
                }
                else
                {
                    DepictInfo = "串行端口打开失败";

                    return IsOpen = false;
                }
            }
            catch
            {
                DepictInfo = "串行端口发生意外打开失败，请检查线路";

                return IsOpen = false;
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public bool CloseSP()
        {
            try
            {
                if(SPserialPort.IsOpen)
                {
                    SPserialPort.Close();

                    DepictInfo = "串行端口关闭成功";

                    return IsOpen = SPserialPort.IsOpen;
                }
                else
                {
                    DepictInfo = "串行端口已关闭";

                    return IsOpen = SPserialPort.IsOpen;
                }
            }
            catch
            {
                DepictInfo = "串行端口发生意外关闭失败，请检查线路";

                return IsOpen = false;
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public void Send()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                if(HexSend)
                {

                }
                else
                {

                }
            }
        }

        /// <summary>
        /// 多项发送
        /// </summary>
        public void Sends()
        {

        }

        /// <summary>
        /// 保存接收 - 路径选择
        /// </summary>
        public void SaveRecePath()
        {
            if(SaveRece)
            {

            }
            else
            {

            }
        }

        /// <summary>
        /// 清空接收区
        /// </summary>
        public void ClarReceData()
        {
            ReceData = string.Empty;
        }

        /// <summary>
        /// 清空发送区
        /// </summary>
        public void ClearSendData()
        {
            SendData = string.Empty;
        }

        /// <summary>
        /// 清空计数
        /// </summary>
        public void ClearCount()
        {
            ReceDataCount = 0;
            SendDataCount = 0;
        }

        /// <summary>
        /// 系统时间
        /// </summary>
        public void SystemTimeData()
        {
            DateTime DTSystemTime = DateTime.Now;

            SystemTime = string.Format("公元{0}年{1}月{2}日 {3}:{4}:{5}",
                DTSystemTime.Year.ToString("0000"),
                DTSystemTime.Month.ToString("00"),
                DTSystemTime.Day.ToString("00"),
                DTSystemTime.Hour.ToString("00"),
                DTSystemTime.Minute.ToString("00"),
                DTSystemTime.Second.ToString("00"));
        }
    }
}
