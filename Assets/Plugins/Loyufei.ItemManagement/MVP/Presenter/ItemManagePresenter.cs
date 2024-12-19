using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using Loyufei.MVP;
using Loyufei.DomainEvents;

namespace Loyufei.ItemManagement
{
    public class ItemManagePresenter<TModel> : ModelPresenter<TModel> where TModel : ItemManageModel
    {
        protected override void RegisterEvents()
        {
            Register<AddItem>     (Add);
            Register<RemoveAtItem>(Remove);
            Register<RemoveItem>  (Remove);
            Register<SwapItem>    (Swap);
            Register<DeliverItem> (Deliver);
        }

        private void Add(AddItem add) 
        {
            var remain = Model.Add(add.Id, add.Count);

            var events = new List<IDomainEvent>() { new UpdateMonitor() };

            if (remain > 0) { events.Add(new AddOverflow(remain)); }

            SettleEvents(events);

            OnManageEnd();
        }

        private void Remove(RemoveAtItem remove)
        {
            var remain = Model.RemoveAt(remove.Index, remove.Count);

            if (remain > 0) { SettleEvents(new RemoveAtOverflow(remain)); }

            OnManageEnd();
        }

        private void Remove(RemoveItem remove) 
        {
            var remain = Model.Remove(remove.Id, remove.Count);

            if (remain > 0) { SettleEvents(new RemoveOverflow(remain)); }

            OnManageEnd();
        }

        private void Swap(SwapItem swap)
        {
            Model.Swap(swap.Index1, swap.Index2);

            OnManageEnd();
        }

        private void Deliver(DeliverItem deliver)
        {
            Model.Deliver(deliver.Id, deliver.Index1, deliver.Index2);

            OnManageEnd();
        }

        protected virtual void OnManageEnd() 
        {

        }
    }
}
