using System;
using System.Collections.Generic;
using System.Text;

namespace Handlers.Models
{
    public class LogIIS : LogBase
    {
        public string RequesterIp { set; get; }
        public RequestMethod Method { set; get; }
        public string Route { set; get; }
        public int Port { set; get; }
        public int StatusCode { set; get; }
    }
}
