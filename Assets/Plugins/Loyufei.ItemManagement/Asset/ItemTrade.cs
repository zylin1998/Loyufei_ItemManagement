using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "Item Trade", menuName = "Loyufei/Inventory/Trade/Item Trade", order = 1)]
    public class ItemTrade : EntityFormAsset<TradeInfo, TradeInfo>, IItemTrade
    {
        public Dictionary<object, TradeInfo> Buffer { get; } = new();

        public virtual TradeInfo Get(object id)
        {
            return Buffer.GetorReturn(id, () =>
            {
                var entity = this[id];

                if (entity.IsDefault()) { return default; }

                Buffer.Add(id, entity.Data);

                return entity.Data;
            });
        }

        public TradeInfo Get(int index) 
        {
            return index >= _Entities.Count ? default : _Entities[index];
        }

        public IEnumerable<TradeInfo> GetAll()
        {
            return this.Select(e => e.Data);
        }
    }
}