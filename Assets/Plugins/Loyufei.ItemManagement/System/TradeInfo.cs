using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [Serializable]
    public class TradeInfo : IEntity<TradeInfo>
    {
        [Serializable]
        public class TradeItem
        {
            [SerializeField]
            private ItemBase _Item;
            [SerializeField]
            private int      _Count = 1;

            public object Id    => _Item.Id;
            public int    Count => _Count;
            public IItem  Item  => _Item;
        }

        [SerializeField]
        private int       _Id;
        [SerializeField]
        private TradeItem _Target;
        [SerializeField]
        private TradeItem _Paid;
        [SerializeField]
        private int       _Capacity;

        public object    Identity => _Id;
        public TradeInfo Data     => this;

        public TradeItem Target   => _Target;
        public TradeItem Paid     => _Paid;
        public int       Capacity => _Capacity > 0 ? _Capacity : int.MinValue;
    }
}
