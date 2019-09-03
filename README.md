### 介绍
---------------------
这是一个适用于Windows（不支持Windows XP）的串行端口调试助手。

![OSerialPort](Docs/images/OserialPort.PNG)

### 软件架构
---------------------
软件采用 WPF [Microsoft .NET Framework 4.6.1](https://www.microsoft.com/zh-CN/download/details.aspx?id=49982) 框架，MVVM 模型开发。

### 功能
---------------------
- [X] 完整的 MVVM 模型
- [X] 基础功能（打开、关闭、接收、发送、清接收区、清发送区和清空计数）
- [X] 十六进制（HEX）接收，十六进制（HEX）发送
- [X] 自动发送
- [X] 保存接收，路径选择
- [ ] 多项发送
- [ ] 在线更新

注：软件串行端口传输的编码方式为 UTF-8 。

### 编译
--------------------
- [ ] VS2015
- [X] VS2017
- [ ] VS2019

```bash
$ git clone https://gitee.com/leven9/OSerialPort.git
$ cd OSerialPort/msvc
```

### 参与贡献
--------------------
```bash
$ git clone https://gitee.com/leven9/OSerialPort.git
```

### 开源执照
---------------------
软件采用 [MIT](https://gitee.com/leven9/OSerialPort/blob/master/LICENSE) 许可授权。