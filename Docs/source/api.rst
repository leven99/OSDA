API参考
########

ITextBoxAppend
***************

.. attribute:: void Delete()

   移除所有字符

.. attribute:: void Delete(int startIndex, int length)

   从 startIndex 开始移除 length 个字符

.. attribute:: void Append(string value)

   追加字符串 value

.. attribute:: void Append(string value, int index)

   将字符串 value 插入到 index 位置

.. attribute:: string GetCurrentValue()

   获取当前值

INotifyPropertyChanged
***********************

.. attribute:: public void RaisePropertyChanged([CallerMemberName] string propertyName = "")

   提示属性已经更改
   
.. code-block:: C#

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