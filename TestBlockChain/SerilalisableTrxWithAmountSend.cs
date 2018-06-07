using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Superstars.Wallet
{
    [Serializable]
    public class SerilalisableTrxWithAmountSend
    {
       List<string> _transactionHash = new List<string>();
            List<decimal> _amount = new List<decimal>();



        public SerilalisableTrxWithAmountSend()
        {
             List<string> TransactionHash = _transactionHash;
             List<decimal> Amount = _amount;


        }

        public List<string> TransactionHash
        {
            get
            {
                return _transactionHash;
            }
            set
            {
                {
                    _transactionHash = value;
                }
            }
        }

        public List<decimal> Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                {
                    _amount = value;
                }
            }
        }

        public void Serialize(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream streamWriter = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(streamWriter, this);
            streamWriter.Close();
        }

        public SerilalisableTrxWithAmountSend Deserialize(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream streamReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            SerilalisableTrxWithAmountSend TrxWithAmountSend = (SerilalisableTrxWithAmountSend)formatter.Deserialize(streamReader);
            streamReader.Close();
            return TrxWithAmountSend;
        }

        public void AddTrxAndAmount(string transactionHash, decimal amount)
        {
            this.TransactionHash.Add(transactionHash);
            this.Amount.Add(amount);
        }

    }
}
