using System;

namespace Superstars.DAL
{
    public class TransferData
    {
        public int TransferId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int Amount { get; set; }

        public int ReceiverId { get; set; }

        public string ReceiverName { get; set; }


        public DateTime TransferDate { get; set; }

    }
}