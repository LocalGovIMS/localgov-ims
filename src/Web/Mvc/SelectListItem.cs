using System;
using System.Collections.Generic;

namespace Web.Mvc
{
    [Serializable]
    public class SelectListItem
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
        public bool Disabled { get; set; }
        public List<ValuePair> DataAttributes { get; set; }

        public SelectListItem()
        {
            DataAttributes = new List<ValuePair>();
        }
    }
}