using System;

namespace Superstars.DAL
{
    public class TransferData
    {
        public int TransferId { get; set; }

        public int UserId { get; set; }

        public int Amount { get; set; }

        public int ReceiverId { get; set; }

        public DateTime TransferDate { get; set; }

    }
}