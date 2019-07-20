using System;
using System.Text;

/*
 * 关于ITextBoxAppend接口定义及接口实现的代码来源：
 * https://stackoverflow.com/questions/27892981/mvvm-how-to-make-a-function-call-on-a-control
 */

namespace OSerialPort.Interface
{
    #region 接口定义
    public interface ITextBoxAppend
    {
        /// <summary>
        /// 移除所有字符
        /// </summary>
        void Delete();

        /// <summary>
        /// 移除指定范围的字符
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        void Delete(int offset, int length);

        /// <summary>
        /// 追加字符串的副本
        /// </summary>
        /// <param name="content"></param>
        void Append(string content);

        /// <summary>
        /// 将字符串插入到指定字符位置
        /// </summary>
        /// <param name="content"></param>
        /// <param name="offset"></param>
        void Append(string content, int offset);

        /// <summary>
        /// 获取当前值
        /// </summary>
        /// <returns></returns>
        string GetCurrentValue();

        /// <summary>
        /// 数据Buffer的附加处理
        /// </summary>
        event EventHandler<string> BufferAppendedHandler;
    }
    #endregion

    #region 接口实现
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
    #endregion
}
