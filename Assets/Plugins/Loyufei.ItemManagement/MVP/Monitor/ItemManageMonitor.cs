using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Loyufei.ItemManagement
{
    public class ItemManageMonitor : IGetter<ItemStack>
    {
        protected virtual void Construct(IItemCollection collection, IItemStorage storage) 
        {
            Collection = collection;
            Storage    = storage;
        }

        public IItemCollection Collection { get; private set; }
        public IItemStorage    Storage    { get; private set; }

        public virtual ItemStack Get(object id) 
        {
            var verified = Collection.Verify(id, out var item);

            return verified ? Get(Storage.Get(id)) : new(default, 0);
        }

        public virtual ItemStack Get(int index) 
        {
            return Get(Storage.Get(index));
        }

        public virtual IEnumerable<ItemStack> GetAll() 
        {
            return Storage.GetAll().Select(Get);
        }

        private ItemStack Get(IItemStorage.IViewer viewer) 
        {
            return new(Collection.Get(viewer.Id), viewer.Count);
        }
    }
}