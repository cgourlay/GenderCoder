using System;
using System.Collections.Generic;

namespace ColinGourlay.GenderEncoder.Utilities
{
    internal class CachedList<T> : IEnumerable<T>
    {
        private TimeSpan? _refreshInterval;
        private List<T> _cachedList;
        private DateTime? _lastRefreshed;

        public CachedList(Func<List<T>> listToCache, TimeSpan? refreshInterval = null)
        {
            _refreshInterval = refreshInterval;
            _cachedList = new List<T>();
            ListToCache = listToCache;
        }

        private List<T> List { get { return GetCachedList(); } }

        private Func<List<T>> ListToCache { get; set; }

        private TimeSpan TimeSinceLastRefresh
        {
            get
            {
                if (_lastRefreshed.HasValue) { return DateTime.Now - _lastRefreshed.Value; }
                return new TimeSpan();
            }
        }

        private List<T> GetCachedList()
        {
            if (_lastRefreshed.HasValue)
            {
                if (!_refreshInterval.HasValue) { return _cachedList; }
                if (TimeSinceLastRefresh <= _refreshInterval) { return _cachedList; }
            }

            UpdateCache();
            return _cachedList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public void UpdateCache()
        {
            _cachedList = ListToCache();
            _lastRefreshed = DateTime.Now;
        }
    }
}
