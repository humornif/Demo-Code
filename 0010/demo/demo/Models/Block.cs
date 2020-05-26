using System;
namespace demo.Models
{
    public class Block
    {
        public Block()
        {
        }
        public DateTimeOffset time_stamp { get; set; }
        public object data { get; set; }
        public string hash { get; set; }
        public string pre_hash { get; set; }
        public string nonce { get; set; }
    }
}
