using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "Item Limitation", menuName = "Loyufei/Inventory/Management/Item Limitation", order = 1)]
    public class ItemLimitation : EntityFormAsset<int, ItemLimitation.Stack>, IItemLimitation
    {
        [Serializable]
        public class Stack : IEntity<int> 
        {
            [SerializeField]
            protected ItemBase _Item;
            [SerializeField]
            protected int      _Limit;

            public object Identity => _Item.Id;
            public int     Data    => _Limit;
        }

        public Dictionary<object, int> Buffer { get; } = new();

        public virtual int Get(object id) 
        {
            return Buffer.GetorAdd(id, () =>
            {
                var entity = this[id];

                return entity.IsDefault() ? int.MaxValue : entity.Data;
            });
        }

        public virtual int Get(int index)
        {
            return index >= _Entities.Count ? 0 : _Entities[index].Data;
        }

        public IEnumerable<int> GetAll() 
        {
            return _Entities.Select(e => e.Data);
        }
    }
}