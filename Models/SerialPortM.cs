using System.IO.Ports;

namespace OSerialPort.Models
{
    class SerialPortM
    {
        public int SPBaudRate { get; set; }
        public int SPDataBits { get; set; }
        public StopBits SPStopbit { get; set; }
        public Parity SPParity { get; set; }
    }
}
