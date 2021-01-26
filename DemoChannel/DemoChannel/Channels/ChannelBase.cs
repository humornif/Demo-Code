using System.Threading.Channels;

namespace DemoChannel.Channels
{
    public sealed class ChannelBase
    {
        private Channel<string> _channel;

        private static ChannelBase _uniqueInstance;
        private static readonly object _locker = new object();

        public static Channel<string> Memo_Channel
        {
            get
            {
                return GetInstance()._channel;
            }
        }

        private ChannelBase()
        {
            _channel = Channel.CreateUnbounded<string>();
        }

        private static ChannelBase GetInstance()
        {
            if (_uniqueInstance == null)
            {
                lock (_locker)
                {
                    if (_uniqueInstance == null)
                    {
                        _uniqueInstance = new ChannelBase();
                    }
                }
            }

            return _uniqueInstance;
        }
    }
}
