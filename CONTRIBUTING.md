## 如何贡献

非常感谢您阅读这篇文章，这个项目需要多的志愿开发者帮助，使这个项目（软件）更加运行稳定、流畅和功能完善。

### 创建议题

在向项目创建一个 `Issue` 时，请确保包含**标题和清晰的描述**。如果有需要，请提供尽可能多的相关信息，以及代码示例或可执行测试用例，以展示未发生的预期行为。

### 提交更改

在向项目发起一个 `Pull Request` 时，请您清楚的列出您完成的具体内容（关于 `Pull Request`的更多信息请 [点击此处](https://help.github.com/cn/articles/proposing-changes-to-your-work-with-pull-requests)）。

请始终为您的 `Commit` 写一条清晰的日志信息。单行的日志信息适用于小的更改。但更大的更改应如下所示：

```bash
$ git commit -a -s -m "A brief summary of the commit
> 
> A paragraph describing what changed and its impact."
```

或（建议使用 `-S` 对您的 `commit` 进行 `GPG` 签名）

```bash
$ git commit -a -S -m "A brief summary of the commit
> 
> A paragraph describing what changed and its impact."
```

对于 `A brief summary of the commit` 请遵守针对互联网公民的简单提交约定（参考：[commitizen](https://github.com/commitizen/cz-cli)）。

### 编码惯例

* 所有的条件代码都需要 `{}`，且 `{` 是另起一行，而不是跟随在语句后面
* 所有的视图（View）使用 `xaml`
* 尽可能的避免在视图（View）中包含逻辑代码

♥♥♥ 非常感谢您对 OSDA 的贡献 ♥♥♥