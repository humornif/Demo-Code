using System;
using System.Threading;
using System.Threading.Tasks;

namespace consoledemo
{
    class Program
    {
        private static AutoResetEvent _exitEvent;

        static async Task Main(string[] args)
        {
            bool isRuned;
            Mutex mutex = new Mutex(true, "OnlyRunOneInstance", out isRuned);
            if (!isRuned)
                return;

            

            _exitEvent = new AutoResetEvent(false);

            await DoWork(_exitEvent);

            _exitEvent.WaitOne();
        }

        private static async Task DoWork(AutoResetEvent _exitEvent)
        {
            /* Your Code Here */

            _exitEvent.Set();
        }
    }
}
