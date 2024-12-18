using System;
using System.Linq;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace Loyufei.ItemManagement
{
    public struct AddItem : IDomainEvent
    {
        public AddItem(object id, int count)
        {
            Id = id;
            Count = count;
        }

        public object Id { get; }
        public int Count { get; }
    }

    public struct AddOverflow : IDomainEvent
    {
        public AddOverflow(int remain)
        {
            Remain = remain;
        }

        public int Remain { get; }
    }
}
