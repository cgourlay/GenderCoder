using System;
using System.Collections.Generic;

namespace ColinGourlay.GenderEncoder.Utilities
{
    internal class CachedList<T> : IEnumerable<T>
    {
        private TimeSpan? _autoRefreshInterval;
        private List<T> _cachedList;
        private DateTime? _lastRefreshed;

        public CachedList(Func<List<T>> listToCache, TimeSpan? autoRefreshInterval = null)
        {
            _autoRefreshInterval = autoRefreshInterval;
            _cachedList = new List<T>();
            ListBeingCached = listToCache;
        }

        private List<T> List
        {
            get
            {
                if (_lastRefreshed.HasValue)
                {
                    if (!_autoRefreshInterval.HasValue) { return _cachedList; }
                    var timeSinceLastRefresh = DateTime.Now - _lastRefreshed.Value;
                    if (timeSinceLastRefresh <= _autoRefreshInterval) { return _cachedList; }
                }

                UpdateCache();
                return _cachedList;
            }
        }

        private Func<List<T>> ListBeingCached { get; set; }

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
            _cachedList = ListBeingCached();
            _lastRefreshed = DateTime.Now;
        }
    }
}
