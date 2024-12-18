using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.ItemManagement
{
    public class ItemTradeMonitor
    {
        protected virtual void Construct(IItemTrade trade, ITradeLog log)
        {
            Trade = trade;
            Log   = log;
        }

        public IItemTrade Trade { get; private set; }
        public ITradeLog  Log   { get; private set; }

        public (TradeInfo info, int remain) Get(object id) 
        {
            var info   = Trade.Get(id);
            var remain = info.Capacity == int.MinValue ? int.MinValue : info.Capacity - Log.Get(id);

            return (info, remain);
        }

        public IEnumerable<(TradeInfo info, int remain)> GetAll() 
        {
            foreach (var info in Trade.GetAll())
            {
                var remain = info.Capacity == int.MinValue ? int.MinValue : info.Capacity - Log.Get(info.Identity);

                yield return (info, remain);
            }
        }
    }
}