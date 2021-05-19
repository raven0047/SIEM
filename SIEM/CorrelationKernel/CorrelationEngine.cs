using System;
using System.Collections.Generic;
using System.Text;
using Handlers;
using Handlers.Models;

namespace CorrelationKernel
{
    public class CorrelationEngine
    {
        List<Rule> _rules;
        ElasticContext _elastic;
        public CorrelationEngine(ElasticContext elastic)
        {
            _rules = new List<Rule>();
            _elastic = elastic;

        }

        public void Start()
        {
           var request = _elastic.Client.Search<LogIIS>(p =>p.
           Index("iislogs").
            Query(d =>d.
            DateRange(r =>r.
            Field(z => z.Date)
            .GreaterThan(new DateTime(2019,01,01))
            .LessThan(DateTime.Now))));
        }
    }
}
