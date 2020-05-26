using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using demo.Models;
using Newtonsoft.Json;

namespace demo
{
    public class BlockChains
    {
        private static SortedList<int, Block> _block_chains = new SortedList<int, Block>();
        private static string _hash_zero = "Initialize_Hash_By_WangPlus";
        private Random _random = new Random((int)DateTimeOffset.Now.Ticks);

        public BlockChains()
        {
        }

        public bool addBlockData(object data)
        {
            Block new_block = new Block()
            {
                time_stamp = DateTimeOffset.Now,
                data = data,
                nonce = $"{_random.Next(9999):D4}",
            };

            new_block.pre_hash = _block_chains.Count <= 0 ? _hash_zero : _block_chains.Last().Value.hash;
            new_block.hash = calculateHash(new_block);

            _block_chains.Add(_block_chains.Count + 1, new_block);

            return true;
        }

        private string calculateHash(Block block)
        {
            if (block == null)
                return string.Empty;

            string data_json = JsonConvert.SerializeObject(block.data, Formatting.None);
            string block_string = $"{block.time_stamp.Ticks.ToString()}|{block.pre_hash}|{data_json}|{block.nonce}";

            var block_hash = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(block_string));

            return Convert.ToBase64String(block_hash);
        }
        private string calculateHash(int index)
        {
            return calculateHash(_block_chains[index]);
        }

        public bool isBlockValid(int index)
        {
            if (index <= 0 || index > _block_chains.Count)
                return false;

            if ((index > 1 && _block_chains[index].pre_hash != _block_chains[index - 1].hash) || (index == 1 && _block_chains[index].pre_hash != _hash_zero))
                return false;

            if (_block_chains[index].hash != calculateHash(index))
                return false;

            return true;
        }
        public SortedList<int, Block> getBlockChains()
        {
            return _block_chains;
        }
    }
}
