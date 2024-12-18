using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.ItemManagement
{
    public interface ITradeLog : IGetter<int>
    {
        public int Preserve(object id, int count, int capacity);
    }
}
