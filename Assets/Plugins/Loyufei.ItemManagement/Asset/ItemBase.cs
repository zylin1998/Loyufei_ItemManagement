using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.ItemManagement
{
    [CreateAssetMenu(fileName = "Item", menuName = "Loyufei/Inventory/Item/Item", order = 1)]
    public class ItemBase : ScriptableObject, IItem, IEntity<IItem>, IDescribe
    {
        [Header("基本道具資訊")]
        [SerializeField]
        protected int    _Id;
        [SerializeField]
        protected string _Name;
        [SerializeField]
        protected Sprite _Icon;
        [SerializeField, TextArea(5, 5)]
        protected string _DescribeFormat;

        public string Name     => _Name;
        public Sprite Icon     => _Icon;
        public object Identity => Id;
        public IItem  Data     => this;

        public virtual object Id          => _Id;
        public virtual string Description => string.Format(_DescribeFormat);
    }
}