using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Loyufei.DomainEvents;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "Useable Item", menuName = "Loyufei/Inventory/Item/Useable Item", order = 1)]
    public class UseableItemBase : ItemBase, IUseableItem
    {
        [SerializeField]
        private bool _RemoveAfterUsed;

        public bool RemoveAfterUsed => _RemoveAfterUsed;

        public IEnumerable<IDomainEvent> GetUseEvents()
        {
            return new IDomainEvent[0];
        }
    }
}
