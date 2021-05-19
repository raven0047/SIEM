using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Handlers.Models;

namespace Handlers
{
    public class IISLogHelper
    {
        public LogIIS TryParse(string log, ref bool flag)
        {
            LogIIS model;
            try {
                model = Parse(log);
            }
            catch (FormatException)
            {
                model = new LogIIS();
                flag = false;
                return model;
            }
            flag = true;
            return model;
        }

        RequestMethod ParseRequestMethod(string item)
        {
            if (item.ToLower() == "post") return RequestMethod.Post;
            if (item.ToLower() == "get") return RequestMethod.Get;
            if (item.ToLower() == "put") return RequestMethod.Put;
            if (item.ToLower() == "delete") return RequestMethod.Delete;
            throw new FormatException();
        }

        // Example: 2021-04-28 18:17:04 192.168.0.103 GET /Home/BadLogin - 80 - 192.168.0.116 200 0 0 3
        LogIIS Parse(string log)
        {
            var model = new LogIIS();
            var items = log.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            IPAddress ipforparse;

            model.Date = DateTime.Parse(items[0] + " " + items[1]);
            ipforparse = IPAddress.Parse(items[2]);
            model.RequesterIp = ipforparse.ToString();
            model.Method = ParseRequestMethod(items[3]);
            model.Route = items[4];
            model.Port = int.Parse(items[6]);
            ipforparse = IPAddress.Parse(items[8]);
            model.OwnerIp = ipforparse.ToString();
            model.StatusCode = int.Parse(items[9]);
            model.HazardLevel = 0;
            return model;
        }
        void Save(LogIIS model)
        {
            throw new NotImplementedException();
        }
    }
}
