using Handlers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handlers
{
    public class MessagePipeline
    {
        ElasticContext _elasticContext;
        IISLogHelper _iishelper;
        public MessagePipeline(ElasticContext elastic, IISLogHelper iis)
        {
            _elasticContext = elastic;
            _iishelper = iis;
        }

        public void OnReceiveMessage_Pipeline(string message)
        {
            var format = LogHelper.DefineFormat(message);
            message = LogHelper.CutFormatPrefix(message);
            switch (format)
            {
                case LogFormatEnum.IIS:
                    {
                        bool parseFlag = true;
                        var model = _iishelper.TryParse(message, ref parseFlag);
                        if (!parseFlag) return;
                        _elasticContext.Save(model);
                    break;
                    }
                case LogFormatEnum.NotDefined:
                    break;
                

            }
        }
    }
}
