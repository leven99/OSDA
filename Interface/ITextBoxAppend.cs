using System;
using System.Text;

/*
 * 关于ITextBoxAppend接口定义、接口实现以及MvvmTextBox封装实现（MainWindows.xaml.cs）的参考代码来源：
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
        /// 移除字符串
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        void Delete(int startIndex, int length);

        /// <summary>
        /// 追加字符串
        /// </summary>
        /// <param name="value"></param>
        void Append(string value);

        /// <summary>
        /// 将字符串插入到指定位置
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        void Append(string value, int index);

        /// <summary>
        /// 获取当前值
        /// </summary>
        /// <returns></returns>
        string GetCurrentValue();

        /// <summary>
        /// 数据Buffer的追加处理
        /// </summary>
        event EventHandler<string> BufferAppendedHandler;

        /// <summary>
        /// 数据Buffer的清空处理
        /// </summary>
        event EventHandler<string> BufferClearingHandler;
    }
    #endregion

    #region 接口实现
    class IClassTextBoxAppend : ITextBoxAppend
    {
        private readonly StringBuilder _buffer = new StringBuilder();

        public void Delete()
        {
            _buffer.Clear();

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                BufferClearingHandler(this, " ");
            }));
        }

        public void Delete(int startIndex, int length)
        {
            _buffer.Remove(startIndex, length);
        }

        public void Append(string value)
        {
            _buffer.Append(value);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                BufferAppendedHandler(this, value);
            }));
        }

        public void Append(string value, int index)
        {
            if (index == _buffer.Length)
            {
                _buffer.Append(value);
            }
            else
            {
                _buffer.Insert(index, value);
            }
        }

        public string GetCurrentValue()
        {
            return _buffer.ToString();
        }

        public event EventHandler<string> BufferAppendedHandler;

        public event EventHandler<string> BufferClearingHandler;
    }
    #endregion
}
