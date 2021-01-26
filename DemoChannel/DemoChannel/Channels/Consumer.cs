using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DemoChannel.Channels
{
    public static class Consumer
    {
        private static readonly ChannelReader<string> _reader;

        public delegate Task OnDataHandler(string data);
        public static event OnDataHandler OnData;

        static Consumer()
        {
            _reader = ChannelBase.Memo_Channel.Reader;
        }

        public static async Task BeginReceive()
        {
            while (await _reader.WaitToReadAsync())
            {
                if (_reader.TryRead(out string data))
                {
                    if (null != OnData)
                        await OnData(data);
                }
            }
        }
    }
}
