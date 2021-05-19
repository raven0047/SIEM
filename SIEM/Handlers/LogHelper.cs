using Handlers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handlers
{
    public class LogHelper
    {
        public static LogFormatEnum DefineFormat(string message)
        {
            string words = message.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
            if (words.ToLower() == "iisconnector") return LogFormatEnum.IIS;
            return LogFormatEnum.NotDefined;
        }
        public static string CutFormatPrefix(string message)
        {
           var index = message.IndexOf(':');
            return message.Substring(index+1);
        }
    }
}
