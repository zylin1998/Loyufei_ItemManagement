using System;
using System.Linq;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei.ItemManagement
{
    public struct DeliverItem : IDomainEvent
    {
        public DeliverItem(object id, int index1, int index2)
        {
            Id = id;
            Index1 = index1;
            Index2 = index2;
        }

        public object Id { get; }
        public int Index1 { get; }
        public int Index2 { get; }
    }
}
