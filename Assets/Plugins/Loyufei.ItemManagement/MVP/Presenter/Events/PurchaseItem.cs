using System;
using System.Linq;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei.ItemManagement
{
    public struct PurchaseItem : IDomainEvent
    {
        public PurchaseItem(object id, int count) 
        {
            Id    = id;
            Count = count;
        }

        public object Id    { get; }
        public int    Count { get; }
    }
}
