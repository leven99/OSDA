using System;
using System.Text;

/*
 * 该接口来源：
 * https://stackoverflow.com/questions/27892981/mvvm-how-to-make-a-function-call-on-a-control
 */

namespace OSerialPort.Interface
{
    public interface ITextBoxAppend
    {
        void Delete();
        void Delete(int offset, int length);

        void Append(string content);
        void Append(string content, int offset);

        string GetCurrentValue();

        event EventHandler<string> BufferAppendedHandler;
    }

    class IClassTextBoxAppend : ITextBoxAppend
    {
        private readonly StringBuilder _buffer = new StringBuilder();

        public void Delete()
        {
            _buffer.Clear();
        }

        public void Delete(int offset, int length)
        {
            _buffer.Remove(offset, length);
        }

        public void Append(string content)
        {
            _buffer.Append(content);

            BufferAppendedHandler?.Invoke(this, content);
        }

        public void Append(string content, int offset)
        {
            if (offset == _buffer.Length)
            {
                _buffer.Append(content);
            }
            else
            {
                _buffer.Insert(offset, content);
            }
        }

        public string GetCurrentValue()
        {
            return _buffer.ToString();
        }

        public event EventHandler<string> BufferAppendedHandler;
    }
}
