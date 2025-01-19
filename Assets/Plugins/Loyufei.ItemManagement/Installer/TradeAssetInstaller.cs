using System.Linq;
using System;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "TradeAssetInstaller", menuName = "Loyufei/Inventory/TradeAssetInstaller")]
    public class TradeAssetInstaller : FileInstallAsset<ITradeLog, TradeAssetInstaller.Channel>
    {
        protected override bool BindChannel(Channel channel)
        {
            Container
                .Bind<IItemTrade>()
                .WithId(channel.Identity)
                .To(channel.Trade.GetType())
                .FromInstance(channel.Trade)
                .AsCached();

            var hasCreate = channel.GetOrCreate(out var instance);

            Container
                .Bind<ITradeLog>()
                .WithId(channel.Identity)
                .To(instance.GetType())
                .FromInstance(instance)
                .AsCached();

            return hasCreate;
        }

        [Serializable]
        public class Channel : Channel<ITradeLog>
        {
            [Header("¸ê·½³sµ²")]
            [SerializeField]
            private ItemTrade _Trade;

            public IItemTrade Trade    => _Trade;
            
            public override bool GetOrCreate(out ITradeLog instance)
            {
                var added = false;

                instance = _Saveable.GetOrAdd(Identity, () =>
                {
                    added = true;

                    return new TradeLog();
                });

                return added;
            }
        }
    }
}