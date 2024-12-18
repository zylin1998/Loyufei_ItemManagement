using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei.DomainEvents;

namespace Loyufei.ItemManagement
{
    public interface IItem 
    {
        public object Id   { get; }
        public string Name { get; }
        public Sprite Icon { get; }
    }

    public interface IUseableItem : IItem
    {
        public bool RemoveAfterUsed { get; }

        public IEnumerable<IDomainEvent> GetUseEvents();
    }
}