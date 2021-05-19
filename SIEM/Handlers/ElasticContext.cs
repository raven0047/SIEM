using Handlers.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Handlers
{
    public class ElasticContext
    {
        ElasticClient _client;
        public ElasticClient Client
        {
            get { return _client; }
        }
        public ElasticContext()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("logs");

            _client = new ElasticClient(settings);
        }
        public void Save()
        {
            var log = new
            {
                Log = "logtext",
                Count = 7
            };
            _client.IndexDocument(log);

        }
        public void Save(LogIIS log)
        {
            _client.Index(log, p => p.Index("iislogs"));
        }

        public void Init()
        {
           if(!_client.Indices.Exists("iislogs").Exists)
            {
                _client.Indices.Create("iislogs", p => p.Map<LogIIS>(m => m.AutoMap()));
               
            }         
        }
    }
}
