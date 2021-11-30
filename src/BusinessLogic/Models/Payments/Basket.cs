using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Models.Payments
{
    [Serializable]
    public class Basket
    {
        public Basket()
        {
            Items = new List<BasketItem>();
        }

        public List<BasketItem> Items { get; set; }

        public Decimal TotalAmount
        {
            get
            {
                return Items.Sum(x => x.Amount);
            }
        }

        public int Count
        {
            get
            {
                if (Items == null) return 0;
                return Items.Count;
            }
        }

        public void RemoveItem(string accountReference)
        {
            Items = Items.Where(x => x.AccountReference != accountReference).ToList();
        }
    }
}