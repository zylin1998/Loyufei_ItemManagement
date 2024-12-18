using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.ItemManagement
{
    public class ItemTradeModel
    {
        protected virtual void Construct(IItemTrade trade, ITradeLog log) 
        {
            Trade = trade;
            Log   = log;
        }

        public IItemTrade Trade { get; private set; }
        public ITradeLog  Log   { get; private set; }
        
        public (TradeInfo info, int preserve, int overflow) Purchase(object id, int count) 
        {
            var info     = Trade.Get(id);
            var overflow = Log.Preserve(id, count, info.Capacity);
            var preserve = count - overflow;

            return (info, preserve, overflow);
        }
    }
}
