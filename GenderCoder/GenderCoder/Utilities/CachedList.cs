using System;
using System.Collections.Generic;

namespace GenderCoder.Utilities
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

        public List<T> List
        {
            get
            {
                bool refreshRequired = true;

                if (_lastRefreshed.HasValue)
                {
                    if (_autoRefreshInterval.HasValue)
                    {
                        TimeSpan TimeSinceLastRefresh = DateTime.Now - _lastRefreshed.Value;

                        if (TimeSinceLastRefresh <= _autoRefreshInterval)
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

        public Func<List<T>> RefreshFunction { get; set; }

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
