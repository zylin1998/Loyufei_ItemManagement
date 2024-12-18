using System;
using System.Collections;
using System.Collections.Generic;

namespace Loyufei.ItemManagement
{
    public interface IItemStorage : IGetter<IItemStorage.IViewer>
    {
        public void Reset(bool isLimit, bool removeRelease, int max, int init);

        public int  Add     (object id, int count, int limit = int.MaxValue);
        public int  Remove  (object id, int count);
        public int  RemoveAt(int index, int count);

        public void Sort   (Comparison<IViewer> comparison);
        public void Swap   (int index1, int index2);
        public void Deliver(int index1, int index2, int limit);

        public interface IViewer
        {
            public object Id         { get; }
            public int    Count      { get; }
            public bool   IsReleased { get; }
        }

        protected class Viewer : IViewer 
        {
            public Viewer(object id, int count, bool isReleased)
            {
                Id         = id;
                Count      = count;
                IsReleased = isReleased;
            }

            public object Id         { get; }
            public int    Count      { get; }
            public bool   IsReleased { get; }
        }
    }
}