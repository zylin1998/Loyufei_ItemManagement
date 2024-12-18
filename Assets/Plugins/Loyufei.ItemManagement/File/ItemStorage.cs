using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Loyufei.ItemManagement
{
    [Serializable]
    public class ItemStorage : FlexibleRepositoryBase<int, int>, IItemStorage
    {
        public ItemStorage() 
        {
            Reset(false, false, 0, 0);
        }

        public ItemStorage(bool limit, bool removeReleased, int max, int init) 
        {
            Reset(limit, removeReleased, max, init);
        }

        public bool RemoveReleased { get; private set; }

        public void Reset(bool limit, bool removeReleased, int max, int init) 
        {
            _Limited     = limit;
            _MaxCapacity = max;

            RemoveReleased = removeReleased;

            Create((init - _Reposits.Count).Clamp(0, init)).ToList();
        }

        public int Add(object id, int count, int stack = int.MaxValue)
        {
            var remain = count;
            
            foreach (var reposit in SearchAll(r => r.Identity.Equals(id))) 
            {
                remain = Add(reposit, remain, stack);
                
                if (remain <= 0) { return 0; }
            }
            
            var requireEmpty = remain / stack + (remain % stack == 0 ? 0 : 1);
            
            foreach (var reposit in GetEmpty(requireEmpty))
            {
                reposit.Set(id);

                remain = Add(reposit, remain, stack);
                
                if (remain <= 0) { return 0; }
            }
            
            return remain;
        }

        public int Remove(object id, int count)
        {
            var remain = count;

            foreach (var reposit in SearchAll(r => r.Identity.Equals(id)))
            {
                remain = Remove(reposit, remain);

                if (reposit.Data <= 0) { reposit.Release(); }

                if (remain <= 0) { return 0; }
            }

            RemoveAllReleased();

            return remain;
        }

        public int RemoveAt(int index, int count)
        {
            if (index >= _Reposits.Count) { return count; }

            var remain = Remove(_Reposits[index], count);

            RemoveAllReleased();

            return remain;
        }

        public void Swap(int index1, int index2) 
        {
            if (index1 >= _Reposits.Count || index2 >= _Reposits.Count) { return; }

            var temp = _Reposits[index1];

            _Reposits[index1] = _Reposits[index2];
            _Reposits[index2] = temp;
        }

        public void Deliver(int index1, int index2, int stack) 
        {
            if (index1 >= _Reposits.Count || index2 >= _Reposits.Count) { return; }
            
            var r1 = _Reposits[index1];
            var r2 = _Reposits[index2];
            
            if (r1.IsReleased) { r1.Set(r2.Identity); }
            
            if (!r1.Identity.Equals(r2.Identity)) { return; }

            var remain = Add(r1, r2.Data, stack);

            r2.Preserve(remain);

            if (r2.Data <= 0) { r2.Release(); }
        }  

        public void Sort(Comparison<IItemStorage.IViewer> comparison) 
        {
            base.Sort((r1, r2) =>
            {
                var viewer1 = new IItemStorage.Viewer(r1.Identity, r1.Data, ((RepositBase<int, int>)r1).IsReleased);
                var viewer2 = new IItemStorage.Viewer(r2.Identity, r2.Data, ((RepositBase<int, int>)r2).IsReleased);

                return comparison(viewer1, viewer2);
            });
        }

        public IItemStorage.IViewer Get(int index)
        {
            var reposit = (RepositBase<int, int>)SearchAt(index);

            return new IItemStorage.Viewer(reposit.Identity, reposit.Data, reposit.IsReleased);
        }

        public IItemStorage.IViewer Get(object id)
        {
            return new IItemStorage.Viewer(id, SearchAll(r => Equals(r.Identity, id)).Sum(r =>r.Data), false);
        }

        public IEnumerable<IItemStorage.IViewer> GetAll()
        {
            foreach (var r in _Reposits) 
            {
                yield return new IItemStorage.Viewer(r.Identity, r.Data, r.IsReleased);
            }
        }

        private IEnumerable<IReposit<int>> GetEmpty(int count)
        {
            var remain = count;

            foreach (var reposit in SearchAll(r => (int)r.Identity <= 0)) 
            {
                yield return reposit;

                if (--remain <= 0){ yield break; }
            }

            foreach (var reposit in Create(remain)) 
            {
                yield return reposit;
            }
        }

        private int Add(IReposit<int> reposit, int remain, int stack) 
        {
            if (reposit.Data >= stack) { return remain; }

            var preserve = Math.Min(remain, stack - reposit.Data);

            reposit.Preserve(reposit.Data + preserve);

            return remain - preserve;
        }

        private int Remove(IReposit<int> reposit, int remain) 
        {
            if (reposit.Data <= 0) { return remain; }

            var preserve = Math.Min(remain, reposit.Data);

            reposit.Preserve(reposit.Data - preserve);

            if (reposit.Data <= 0) { reposit.Release(); }

            return remain - preserve;
        }

        private int RemoveAllReleased() 
        {
            if (!RemoveReleased) { return 0; }

            return _Reposits.RemoveAll(r => r.IsReleased);
        }
    }
}
