using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    public class ChatData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TextMessage { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }
    }
}
