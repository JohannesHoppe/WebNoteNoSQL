using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace WebNoteTests.ExplorativeTests.RavenDb
{
    public class SearchIndex : AbstractMultiMapIndexCreationTask<SearchIndex.Result>
    {
        public class Result
        {
            public string Country { get; set; }
            public int Size { get; set; }
            public int Count { get; set; }
        }

        public SearchIndex()
        {
            AddMap<LogEntry>(logEntries => from entry in logEntries
                                           select new Result
                                           { 
                                               Country = entry.Country,
                                               Size = entry.Size,
                                               Count = 1
                                           });

            Reduce = entries => from entry in entries
                                group entry by entry.Country into e
                                select new Result
                                {
                                    Country = e.Key,
                                    Count = e.Sum(x => x.Count),
                                    Size = e.Sum(x => x.Size),
                                };

            Indexes.Add(x => x.Country, FieldIndexing.Analyzed);
        }
    }
}
