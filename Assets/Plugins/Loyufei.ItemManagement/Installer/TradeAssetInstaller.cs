using System.Linq;
using System;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "TradeAssetInstaller", menuName = "Loyufei/Inventory/TradeAssetInstaller")]
    public class TradeAssetInstaller : FileInstallAsset<TradeAssetInstaller.Saveable, TradeAssetInstaller.Channel>
    {
        protected override bool BindChannel(int index, Channel channel)
        {
            Container
                .Bind<IItemTrade>()
                .WithId(channel.Identity)
                .To(channel.Trade.GetType())
                .FromInstance(channel.Trade)
                .AsCached();

            var instance = channel.GetOrCreate(index, out var hasCreate);

            Container
                .Bind<ITradeLog>()
                .WithId(channel.Identity)
                .To(instance.GetType())
                .FromInstance(instance)
                .AsCached();

            return hasCreate;
        }

        [Serializable]
        public class Saveable : IAdjustableSaveable<ITradeLog>
        {
            [SerializeField]
            private TradeLog[] _TradeLogs = new TradeLog[0];

            public ITradeLog GetOrAdd(int index, Func<ITradeLog> add)
            {
                if (index < _TradeLogs.Length) { return _TradeLogs[index]; }

                if (index > _TradeLogs.Length) { return default; }

                var log = add.Invoke();

                _TradeLogs = _TradeLogs.Append((TradeLog)log).ToArray();

                return _TradeLogs[index];
            }
        }

        [Serializable]
        public class Channel : Channel<ITradeLog>
        {
            [Header("¸ê·½³sµ²")]
            [SerializeField]
            private ItemTrade _Trade;

            public IItemTrade Trade    => _Trade;
            
            public override object GetOrCreate(int index, out bool hasCreate)
            {
                var added = false;

                var instance = _Saveable.GetOrAdd(index, () =>
                {
                    added = true;

                    return new TradeLog();
                });

                hasCreate = added;

                return instance;
            }
        }
    }
}