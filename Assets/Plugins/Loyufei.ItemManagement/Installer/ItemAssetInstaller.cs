using System;
using System.Linq;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "ItemAssetInstaller", menuName = "Loyufei/Inventory/ItemAssetInstaller")]
    public class ItemAssetInstaller : FileInstallAsset<ItemAssetInstaller.Saveable, ItemAssetInstaller.Channel>
    {
        protected override bool BindChannel(int index, Channel channel)
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

            var instance = channel.GetOrCreate(index, out var hasCreate);

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

            public override object GetOrCreate(int index, out bool hasCreate)
            {
                var added = false;
                
                var instance = _Saveable.GetOrAdd(index, () =>
                {
                    added = true;

                    return new ItemStorage();
                });

                hasCreate = added;

                instance.Reset(IsLimit, RemoveReleased, MaxCapacity, InitCapacity);

                return instance;
            }
        }

        [Serializable]
        public class Saveable : IAdjustableSaveable<IItemStorage>
        {
            [SerializeField]
            private ItemStorage[] _Storages = new ItemStorage[0];

            public IItemStorage GetOrAdd(int index, Func<IItemStorage> add)
            {
                if (index < _Storages.Length) { return _Storages[index]; }

                if (index > _Storages.Length) { return default; }

                var storage = add.Invoke();

                _Storages = _Storages.Append((ItemStorage)storage).ToArray();

                return _Storages[index];
            }
        }
    }
}