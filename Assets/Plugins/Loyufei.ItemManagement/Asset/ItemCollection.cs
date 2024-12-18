using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "Item Collection", menuName = "Loyufei/Inventory/Management/Item Collection", order = 1)]
    public class ItemCollection : EntityFormAsset<IItem, ItemBase>, IItemCollection
    {
        public Dictionary<object, IItem> Buffer { get; } = new();

        public virtual IItem Get(object id)
        {
            return Buffer.GetorReturn(id, () =>
            {
                var entity = this[id];

                if (entity.IsDefault()) { return default; }

                Buffer.Add(id, entity.Data);

                return entity.Data;
            });
        }

        public virtual IItem Get(int index) 
        {
            return index >= _Entities.Count ? default : _Entities[index];
        }

        public virtual IEnumerable<IItem> GetAll()
        {
            foreach (var entity in _Entities) 
            {
                yield return entity.Data;
            }
        }

        public bool Verify(object id, out IItem item)
        {
            item = Get(id);

            return !item.IsDefault();
        }
    }
}
