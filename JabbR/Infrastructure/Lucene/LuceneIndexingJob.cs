﻿using System;
using System.Threading.Tasks;
using WebBackgrounder;

namespace JabbR.Infrastructure
{
    public class LuceneIndexingJob : Job
    {
        private readonly ISearchIndexingService _searchIndexingService;

        public LuceneIndexingJob(TimeSpan interval, ISearchIndexingService searchIndexingService)
            : base("SearchIndexing", interval)
        {
            _searchIndexingService = searchIndexingService;

            // Updates the index synchronously first time job is created.
            try
            {
                _searchIndexingService.UpdateIndex();
            }
            catch
            {
            }
        }

        public override Task Execute()
        {
            return new Task(_searchIndexingService.UpdateIndex);
        }
    }
}