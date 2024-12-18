using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.ItemManagement
{
    public interface IItemCollection : IGetter<IItem>
    {
        bool Verify(object id, out IItem item);
    }
}
