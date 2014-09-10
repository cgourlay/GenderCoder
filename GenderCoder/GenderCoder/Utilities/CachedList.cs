using System;
using System.Collections.Generic;

namespace GenderCoder.Utilities
{
    internal class CachedList<T> : IEnumerable<T>
    {
        private DateTime? LastRefreshed = null;

        private TimeSpan? AutoRefreshInterval;

        public CachedList(Func<List<T>> RefreshFunction, TimeSpan? AutoRefreshInterval = null)
        {
            this.RefreshFunction = RefreshFunction;
            this.AutoRefreshInterval = AutoRefreshInterval;

            _CachedList = new List<T>();

        }

        public Func<List<T>> RefreshFunction { get; set; }

        public void Refresh()
        {
            LastRefreshed = DateTime.Now;

            _CachedList = RefreshFunction();

            LastRefreshed = DateTime.Now;
        }

        private List<T> _CachedList;

        public List<T> List
        {
            get
            {
                bool RefreshRequired = true;

                if (LastRefreshed.HasValue)
                {
                    if (AutoRefreshInterval.HasValue)
                    {
                        TimeSpan TimeSinceLastRefresh = DateTime.Now - LastRefreshed.Value;

                        if (TimeSinceLastRefresh <= AutoRefreshInterval)
                        {
                            RefreshRequired = false;
                        }
                    }
                    else
                    {
                        RefreshRequired = false;
                    }
                }

                if (RefreshRequired)
                {
                    this.Refresh();
                }

                return _CachedList;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }
    }
}
