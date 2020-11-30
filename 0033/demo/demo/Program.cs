using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChannelSample
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Which sample do you want to run?");
            Console.WriteLine("1. Single producer, single consumer");
            Console.WriteLine("2. Multiple producers, single consumer");
            Console.WriteLine("3. Single producer, multiple consumers");

            var key = Console.ReadKey();

            switch (key.KeyChar)
            {
                case '1':
                    await SingleProducerSingleConsumer();
                    break;

                case '2':
                    await MultiProducerSingleConsumer();
                    break;

                case '3':
                    await SingleProduceMultipleConsumers();
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        public static async Task SingleProducerSingleConsumer()
        {
            var channel = Channel.CreateUnbounded<string>();

            var producer1 = new Producer(channel.Writer, 1, 2000);
            var consumer1 = new Consumer(channel.Reader, 1, 1500);

            Task consumerTask1 = consumer1.ConsumeData(); // begin consuming
            Task producerTask1 = producer1.BeginProducing(); // begin producing

            await producerTask1.ContinueWith(_ => channel.Writer.Complete());

            await consumerTask1;
        }

        public static async Task MultiProducerSingleConsumer()
        {
            var channel = Channel.CreateUnbounded<string>();

            var producer1 = new Producer(channel.Writer, 1, 2000);
            var producer2 = new Producer(channel.Writer, 2, 2000);
            var consumer1 = new Consumer(channel.Reader, 1, 250);

            Task consumerTask1 = consumer1.ConsumeData(); // begin consuming

            Task producerTask1 = producer1.BeginProducing();

            await Task.Delay(500); // stagger the producers

            Task producerTask2 = producer2.BeginProducing();

            await Task.WhenAll(producerTask1, producerTask2)
                .ContinueWith(_ => channel.Writer.Complete());

            await consumerTask1;
        }

        public static async Task SingleProduceMultipleConsumers()
        {
            var channel = Channel.CreateUnbounded<string>();

            var producer1 = new Producer(channel.Writer, 1, 100);
            var consumer1 = new Consumer(channel.Reader, 1, 1500);
            var consumer2 = new Consumer(channel.Reader, 2, 1500);
            var consumer3 = new Consumer(channel.Reader, 3, 1500);

            Task consumerTask1 = consumer1.ConsumeData(); // begin consuming
            Task consumerTask2 = consumer2.ConsumeData(); // begin consuming
            Task consumerTask3 = consumer3.ConsumeData(); // begin consuming

            Task producerTask1 = producer1.BeginProducing();

            await producerTask1.ContinueWith(_ => channel.Writer.Complete());

            await Task.WhenAll(consumerTask1, consumerTask2, consumerTask3);
        }
    }

    internal class Producer
    {
        private readonly ChannelWriter<string> _writer;
        private readonly int _identifier;
        private readonly int _delay;

        public Producer(ChannelWriter<string> writer, int identifier, int delay)
        {
            _writer = writer;
            _identifier = identifier;
            _delay = delay;
        }

        public async Task BeginProducing()
        {
            Console.WriteLine($"PRODUCER ({_identifier}): Starting");

            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(_delay); // simulate fetching some data

                var msg = $"P{_identifier} - {DateTime.Now:G}";

                Console.WriteLine($"PRODUCER ({_identifier}): Creating {msg}");

                await _writer.WriteAsync(msg);
            }

            Console.WriteLine($"PRODUCER ({_identifier}): Completed");
        }
    }

    internal class Consumer
    {
        private readonly ChannelReader<string> _reader;
        private readonly int _identifier;
        private readonly int _delay;

        public Consumer(ChannelReader<string> reader, int identifier, int delay)
        {
            _reader = reader;
            _identifier = identifier;
            _delay = delay;
        }

        public async Task ConsumeData()
        {
            Console.WriteLine($"CONSUMER ({_identifier}): Starting");

            while (await _reader.WaitToReadAsync())
            {
                if (_reader.TryRead(out var timeString))
                {
                    await Task.Delay(_delay); // simulate processing time

                    Console.WriteLine($"CONSUMER ({_identifier}): Consuming {timeString}");
                }
            }

            Console.WriteLine($"CONSUMER ({_identifier}): Completed");
        }
    }
}