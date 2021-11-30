using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Mvc
{
    [Serializable]
    public class SelectList
    {
        public List<SelectListItem> Items { get; set; }

        public List<System.Web.Mvc.SelectListItem> ToSelectList()
        {
            return this.Items.Select(i => new System.Web.Mvc.SelectListItem
            {
                Value = i.Value,
                Text = i.Text,
                Selected = i.Selected
            }).ToList();
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectList(bool includeIdInText)
        {
            if (!includeIdInText) return ToSelectList();

            return this.Items.Select(i => new System.Web.Mvc.SelectListItem
            {
                Value = i.Value,
                Text = string.Format("{1} - ({0})", i.Value, i.Text),
                Selected = i.Selected
            }).ToList();
        }

        public SelectList()
        {
            Items = new List<SelectListItem>();
        }

        public SelectList(IEnumerable<System.Web.Mvc.SelectListItem> list)
        {
            Items = list.Select(item => new SelectListItem()
            {
                Selected = item.Selected,
                Value = item.Value,
                Text = item.Text
            }).ToList();
        }

        public SelectList(IEnumerable<System.Web.Mvc.SelectListItem> list, bool includeIdInText)
        {
            if (includeIdInText)
            {
                Items = list.Select(item => new SelectListItem()
                {
                    Value = item.Value,
                    Text = string.Format("{1} - ({0})", item.Value, item.Text),
                    Selected = item.Selected
                }).ToList();
            }
            else
            {
                Items = new SelectList(list).Items;
            }
        }

        public SelectList(IEnumerable<SelectListItem> list, bool includeIdInText)
        {
            if (includeIdInText)
            {
                Items = list.Select(item => new SelectListItem()
                {
                    Value = item.Value,
                    Text = string.Format("{1} - ({0})", item.Value, item.Text),
                    Selected = item.Selected,
                    DataAttributes = item.DataAttributes
                }).ToList();
            }
            else
            {
                Items = list.ToList();
            }
        }
    }
}