using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.FundGroup
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [DisplayName("Fund group name")]
        [Required(ErrorMessage = "Fund group name is required")]
        public string FundGroupName { get; set; }

        public ICollection<CheckBoxListItem> Funds { get; set; }
    }
}