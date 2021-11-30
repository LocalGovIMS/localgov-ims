using BusinessLogic.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace Admin.Models.FundGroup
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Fund group name")]
        public string FundGroupName { get; set; }

        public List<FundGroupFund> Funds { get; set; }
    }
}