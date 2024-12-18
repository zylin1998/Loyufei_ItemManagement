using JetBrains.Annotations;
using Loyufei.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei.ItemManagement
{
    public class ItemTradePresenter<TModel> : ModelPresenter<TModel> where TModel : ItemTradeModel
    {
        protected override void RegisterEvents()
        {
            Register<PurchaseItem>(Purchase);
        }

        private void Purchase(PurchaseItem purchase) 
        {
            var result = Model.Purchase(purchase.Id, purchase.Count);

            if (result.overflow > 0) { }

            SendAddEvent(result.info, result.preserve);
        }

        protected virtual void SendAddEvent(TradeInfo info, int count) 
        {
            var (target,    add) = (info.Target.Item, info.Target.Count * count);
            var (paid  , remove) = (info.Paid  .Item, info.Paid  .Count * count);

            var addItem    = new AddItem   (target.Id, add);
            var removeItem = new RemoveItem(paid.Id  , remove);

            SettleEvents(addItem, removeItem);
        }
    }
}
