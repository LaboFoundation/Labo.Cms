namespace Labo.Cms.Core.Routing
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class RouteInfoCollection : List<RouteInfo>
    {
        public RouteInfoCollection()
            : base()
        {
        }

        public RouteInfoCollection(IEnumerable<RouteInfo> collection)
            : base(collection)
        {
        }

        public RouteInfoCollection(int capacity)
            : base(capacity)
        {
        }

        public void SortByPriority()
        {
            Sort((x, y) => Comparer<int>.Default.Compare(x.Priority, y.Priority));
        }
    }
}