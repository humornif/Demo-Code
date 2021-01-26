你那个程序我看了，没什么大毛病，主要是几个线程的流程有点乱。

&emsp;

在这个DEMO中：

`ChannelBase.cs` - 这儿我对Channel做了一个单例。这个不是必须的，你用个static声明也行。我习惯这么做。

`IProducer.cs`和`Producer.cs` - 为了注入到API中用的。也是我的习惯。实际也可以直接做个类去调用。

`Consumer.cs` - 这个是消费类，里面重要的部分是做了一个`event`，这样可以把接收消息的处理异步放到外面。

`DataHandle.cs` - 这个是真正的接收消息后的处理方法。在这儿可以实现你自己的内容。

`WeatherForecastController.cs` - 这是模板中的API，我在这里做了生产者的发送。

最重要的是`Program.cs`中，你原来那个写法有问题。应该是这么写，把接收消息的循环放到一个线程中。