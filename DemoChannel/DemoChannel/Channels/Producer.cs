using System.Threading.Channels;
using System.Threading.Tasks;

namespace DemoChannel.Channels
{
    public class Producer : IProducer
    {
        private readonly ChannelWriter<string> _writer;

        public Producer()
        {
            _writer = ChannelBase.Memo_Channel.Writer;
        }

        public async Task<bool> SendAsync(string data)
        {
            await _writer.WriteAsync(data);

            return true;
        }
    }
}
