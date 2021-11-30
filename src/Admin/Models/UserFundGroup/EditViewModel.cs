﻿using System.Collections.Generic;
using System.ComponentModel;
using Web.Mvc;

namespace Admin.Models.UserFundGroup
{
    public class EditViewModel
    {
        public int UserId { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public ICollection<CheckBoxListItem> FundGroups { get; set; }
    }
}