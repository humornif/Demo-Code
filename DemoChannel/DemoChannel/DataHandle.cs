using System;
using System.Threading.Tasks;

namespace DemoChannel
{
    public static class DataHandle
    {
        internal static async Task Handler(string data)
        {
            Console.WriteLine($"{DateTime.Now.ToString()} - {data}");
        }
    }
}
