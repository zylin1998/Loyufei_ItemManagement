using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.ItemManagement
{
    [Serializable]
    public class TradeLog : FlexibleRepositoryBase<int, int>, ITradeLog
    {
        public int Preserve(object id, int count, int capacity) 
        {
            var reposit = GetOrAdd(id);

            var add = reposit.Data + count;

            reposit.Preserve(add.Clamp(0, capacity));

            return (add - capacity).Clamp(0, count);
        }

        public int Get(object id)
        {
            var reposit = SearchAt(id);

            return reposit.IsDefault() ? 0 : reposit.Data;
        }

        public int Get(int index)
        {
            var reposit = SearchAt(index);

            return reposit.IsDefault() ? 0 : reposit.Data;
        }

        public IEnumerable<int> GetAll()
        {
            return _Reposits.Select(r => r.Data);
        }

        private IReposit<int> GetOrAdd(object id) 
        {
            var reposit = SearchAt(id);

            if (reposit.IsDefault()) 
            {
                reposit = new RepositBase<int, int>();

                _Reposits.Add((RepositBase<int, int>)reposit);

                reposit.Set(id);
            }

            return reposit;
        }
    }
}
