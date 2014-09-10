using System;
using System.Collections.Generic;

namespace ColinGourlay.GenderEncoder.Utilities
{
    internal class CachedList<T> : IEnumerable<T>
    {
        private TimeSpan? _autoRefreshInterval;
        private List<T> _cachedList;
        private DateTime? _lastRefreshed;

        public CachedList(Func<List<T>> refreshFunction, TimeSpan? autoRefreshInterval = null)
        {
            _autoRefreshInterval = autoRefreshInterval;
            _cachedList = new List<T>();
            RefreshFunction = refreshFunction;
        }

        private List<T> List
        {
            get
            {
                bool refreshRequired = true;

                if (_lastRefreshed.HasValue)
                {
                    if (_autoRefreshInterval.HasValue)
                    {
                        TimeSpan timeSinceLastRefresh = DateTime.Now - _lastRefreshed.Value;

                        if (timeSinceLastRefresh <= _autoRefreshInterval)
                        {
                            refreshRequired = false;
                        }
                    }
                    else
                    {
                        refreshRequired = false;
                    }
                }

                if (refreshRequired)
                {
                    Refresh();
                }

                return _cachedList;
            }
        }

        private Func<List<T>> RefreshFunction { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public void Refresh()
        {
            _lastRefreshed = DateTime.Now;
            _cachedList = RefreshFunction();
            _lastRefreshed = DateTime.Now;
        }
    }
}
