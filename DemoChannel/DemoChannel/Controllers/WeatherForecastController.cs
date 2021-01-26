using System.Threading.Tasks;
using DemoChannel.Channels;
using Microsoft.AspNetCore.Mvc;

namespace DemoChannel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static IProducer _producer;

        public WeatherForecastController(IProducer producer)
        {
            _producer = producer;
        }

        [HttpGet]
        public async Task<bool> Get()
        {
            await _producer.SendAsync(@"hello");

            return true;
        }
    }
}
