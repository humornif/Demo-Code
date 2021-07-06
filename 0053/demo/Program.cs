using System;
using CommandLineTool;

namespace demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Cli cli = new Cli(typeof(TestCLI))
            {
                Introduction = "这是一个 Demo 应用",
                PromptText = "WangPlus",
            };

            cli.SetCancellationKeys(new() { "exit" });
            cli.Start();
        }
    }
}
