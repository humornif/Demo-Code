using System.Threading.Tasks;

namespace DemoChannel.Channels
{
    public interface IProducer
    {
        Task<bool> SendAsync(string data);
    }
}
