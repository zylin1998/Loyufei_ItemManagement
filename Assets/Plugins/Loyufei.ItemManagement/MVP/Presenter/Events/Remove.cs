using System;
using System.Linq;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei.ItemManagement
{
    public struct RemoveAtItem : IDomainEvent
    {
        public RemoveAtItem(int index, int count)
        {
            Index = index;
            Count = count;
        }

        public int Index { get; }
        public int Count { get; }
    }

    public struct RemoveItem : IDomainEvent
    {
        public RemoveItem(object id, int count)
        {
            Id = id;
            Count = count;
        }

        public object Id { get; }
        public int Count { get; }
    }

    public struct RemoveAtOverflow : IDomainEvent
    {
        public RemoveAtOverflow(int remain)
        {
            Remain = remain;
        }

        public int Remain { get; }
    }

    public struct RemoveOverflow : IDomainEvent
    {
        public RemoveOverflow(int remain)
        {
            Remain = remain;
        }

        public int Remain { get; }
    }
}
