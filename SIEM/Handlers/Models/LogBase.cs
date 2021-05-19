using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Handlers.Models
{
    public abstract class LogBase
    {
        public DateTime Date { set; get; }
        public string OwnerIp { set; get; }
        public int HazardLevel { set; get; }
    }
}
