using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace Loyufei.ItemManagement
{
    public class ItemManageModel
    {
        protected virtual void Construct(IItemStorage storage, IItemCollection collection, IItemLimitation limitation) 
        {
            Storage    = storage;
            Collection = collection;
            Limitation = limitation;
        }

        public IItemStorage    Storage    { get; private set; }
        public IItemCollection Collection { get; private set; }
        public IItemLimitation Limitation { get; private set; }

        public bool Verify(object id, out IItem item) 
        {
            return Collection.Verify(id, out item);
        }

        public IItem Get(int index) 
        {
            var viewer = Storage.Get(index);

            return Verify(viewer.Id, out var item) ? item : default;
        }

        public int Add(object id, int count) 
        {
            if (!Verify(id, out var item)) { return count; }
            
            return Storage.Add(id, count, Limitation.Get(id));
        }

        public int Remove(object id, int count) 
        {
            if (!Verify(id, out var item)) { return count; }

            return Storage.Remove(id, count);
        }

        public int RemoveAt(int index, int count)
        {
            return Storage.RemoveAt(index, count);
        }

        public void Swap(int index1, int index2) 
        {
            Storage.Swap(index1, index2);
        }

        public void Deliver(object id, int index1, int index2) 
        {
            if (!Verify(id, out var item)) { return; }

            var stack = Limitation.Get(id);

            Storage.Deliver(index1, index2, stack);
        }

        public void Sort(Comparison<IItem> comparison) 
        {
            Storage.Sort((r1, r2) =>
            {
                var (item1, item2) = (Collection.Get((int)r1.Id), Collection.Get((int)r2.Id));

                return comparison(item1, item2);
            });
        }
    }
}
