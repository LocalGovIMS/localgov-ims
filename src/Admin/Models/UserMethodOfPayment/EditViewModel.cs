using System.Collections.Generic;
using System.ComponentModel;
using Web.Mvc;

namespace Admin.Models.UserMethodOfPayment
{
    public class EditViewModel
    {
        public int UserId { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public ICollection<CheckBoxListItem> MopCodes { get; set; }
    }
}