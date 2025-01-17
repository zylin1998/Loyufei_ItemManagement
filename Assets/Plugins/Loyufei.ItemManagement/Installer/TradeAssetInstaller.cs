using System.Linq;
using System;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "TradeAssetInstaller", menuName = "Loyufei/Inventory/TradeAssetInstaller")]
    public class TradeAssetInstaller : FileInstallAsset<TradeLog, TradeAssetInstaller.Channel>
    {
        protected override bool BindChannel(Channel channel)
        {
            Container
                .Bind<IItemTrade>()
                .WithId(channel.Identity)
                .To(channel.Trade.GetType())
                .FromInstance(channel.Trade)
                .AsCached();

            var instance = channel.GetOrCreate(out var hasCreate);

            Container
                .Bind<ITradeLog>()
                .WithId(channel.Identity)
                .To(instance.GetType())
                .FromInstance(instance)
                .AsCached();

            return hasCreate;
        }

        [Serializable]
        public new class Channel : FileInstallAsset<TradeLog, Channel>.Channel
        {
            [Header("¸ê·½³sµ²")]
            [SerializeField]
            private ItemTrade _Trade;

            public IItemTrade Trade    => _Trade;
            
            public override object GetOrCreate(out bool hasCreate)
            {
                var added = false;

                var instance = _Saveable.GetOrAdd(Identity, () =>
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