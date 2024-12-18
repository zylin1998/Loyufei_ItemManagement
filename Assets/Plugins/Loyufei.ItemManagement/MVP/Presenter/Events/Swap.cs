using System;
using System.Linq;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei.ItemManagement
{
    public struct SwapItem : IDomainEvent
    {
        public SwapItem(int index1, int index2)
        {
            Index1 = index1;
            Index2 = index2;
        }

        public int Index1 { get; }
        public int Index2 { get; }
    }
}
