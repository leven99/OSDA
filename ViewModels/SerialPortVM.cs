using OSerialPort.Models;
using System.IO.Ports;

namespace OSerialPort.ViewModels
{
    class SerialPortVM
    {
        public SerialPortM MSerialPortM { get; set; }

        /// <summary>
        /// 查找串行端口
        /// </summary>
        public void SPFindPort()
        {
            SerialPort.GetPortNames();
        }
    }
}
