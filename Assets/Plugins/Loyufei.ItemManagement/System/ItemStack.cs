using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.ItemManagement
{
    public struct ItemStack
    {
        public ItemStack(IItem item, int count)
            => (Item, Count) = (item, count);

        public IItem Item { get; }
        public int Count { get; }
    }
}
