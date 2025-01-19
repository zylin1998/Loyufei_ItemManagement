using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "ItemAssetInstaller", menuName = "Loyufei/Inventory/ItemAssetInstaller")]
    public class ItemAssetInstaller : FileInstallAsset<IItemStorage, ItemAssetInstaller.Channel>
    {
        protected override bool BindChannel(Channel channel)
        {
            Container
                .Bind<IItemCollection>()
                .WithId(channel.Identity)
                .To(channel.Collection.GetType())
                .FromInstance(channel.Collection)
                .AsCached();

            Container
                .Bind<IItemLimitation>()
                .WithId(channel.Identity)
                .To(channel.Limitation.GetType())
                .FromInstance(channel.Limitation)
                .AsCached();

            var hasCreate = channel.GetOrCreate( out var instance);

            Container
                .Bind<IItemStorage>()
                .WithId(channel.Identity)
                .To(instance.GetType())
                .FromInstance(instance)
                .AsCached();

            return hasCreate;
        }

        [Serializable]
        public class Channel : Channel<IItemStorage>
        {
            [Header("資源連結")]
            [SerializeField]
            private ItemCollection _Collection;
            [SerializeField]
            private ItemLimitation _Limitation;
            [Header("背包設定")]
            [SerializeField]
            private bool _IsLimit;
            [SerializeField]
            private bool _RemoveReleased;
            [SerializeField]
            private int _MaxCapacity;
            [SerializeField]
            private int _InitCapacity;

            public IItemCollection Collection => _Collection;
            public IItemLimitation Limitation => _Limitation;

            public bool IsLimit        => _IsLimit;
            public bool RemoveReleased => _RemoveReleased;
            public int MaxCapacity  => _MaxCapacity;
            public int InitCapacity => _InitCapacity;

            public override bool GetOrCreate(out IItemStorage instance)
            {
                var result = false;
                
                instance = _Saveable.GetOrAdd(Identity, () =>
                {
                    result = true;

                    return new ItemStorage();
                });

                instance.Reset(IsLimit, RemoveReleased, MaxCapacity, InitCapacity);

                return result;
            }
        }
    }
}