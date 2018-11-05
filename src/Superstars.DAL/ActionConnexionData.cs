using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    [Serializable()]
    public class ActionConnexionData
    {
        public int UserID { get; set; }

        public DateTime ConnexionDate { get; set; }
    }
}
