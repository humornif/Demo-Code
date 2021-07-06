using System;
using System.Collections.Generic;
using System.IO;
using CommandLineTool.Attributes;

namespace demo
{
    [App("Demo")]
    public class TestCLI
    {
        [Command("hello", "就是打个招呼")]
        public static void Hello(string name)
        {
            Console.WriteLine($"Hello {name}");
        }

        [Command("multiinput", "多个参数")]
        public static void MultiInput([ParamArgument()] List<string> names)
        {
            foreach (var item in names)
            {
                Console.WriteLine($"Hello {item}");
            }
        }
        [Command("multifile", "多个文件")]
        public static void MultiFile([ParamArgument()] List<FileInfo> files)
        {
            foreach (var item in files)
            {
            }
        }
        [Command("withpara", "额外参数")]
        public static void WithPara([ParamArgument()] string names, [ParamOption("-a")] string op1)
        {
        }
    }
}
