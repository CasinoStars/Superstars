using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    [Serializable()]
    public class ActionChangeSeedData
    {
        public int UserID { get; set; }

        public DateTime Date { get; set; }

        public string NewClientSeed { get; set; }

        public string PreviousClientSeed { get; set; }

    }
}
