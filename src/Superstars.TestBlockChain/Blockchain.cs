using System;
using System.Collections.Generic;
using System.Linq;
using QBitNinja.Client.Models;

namespace TestBlockChain
{
    [Serializable]
    internal class Blockchain
    {
        // a chain has many blocks
        public List<Block> _chain;

        public Blockchain()
        {
            _chain = new List<Block>();
            _chain.Add(CreateGenesisBlock());
            Difficulty = 2;
        }

        public int Difficulty { get; set; }

        private Block CreateGenesisBlock()
        {
            return new Block(0, "01/12/2017", new List<GetTransactionResponse>(), "");
        }

        public Block GetLatestBlock()
        {
            return _chain.Last();
        }

        // Once a block is mined, add it to the block
        public void AddBlock(Block newBlock)
        {
            newBlock.PreviousHash = GetLatestBlock().Hash;
            newBlock.Mine(Difficulty);

            _chain.Add(newBlock);
        }

        public void ValidateChain()
        {
            for (var i = 1; i < _chain.Count; i++)
            {
                var currentBlock = _chain[i];
                var previousBlock = _chain[i - 1];

                // Check if the current block hash is consistent with the hash calculated
                if (currentBlock.Hash != currentBlock.CalculateHash())
                    throw new Exception("Chain is not valid! Current hash is incorrect!");

                // Check if the Previous hash match the hash of previous block
                if (currentBlock.PreviousHash != previousBlock.Hash)
                    throw new Exception(
                        "Chain is not valid! PreviousHash isn't pointing to the previous block's hash!");
            }
        }
    }
}