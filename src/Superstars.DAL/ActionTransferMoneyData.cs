using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    [Serializable()]
    public class ActionTransferMoneyData
    {
        public int FromUserID { get; set; }

        public int ToUserID { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
