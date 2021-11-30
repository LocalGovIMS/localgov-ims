using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Admin.Models.User
{
    public class DetailsViewModel
    {
        public int UserId { get; set; }

        [DisplayName("User name")]
        public string UserName { get; set; }

        [DisplayName("Last login")]
        public DateTime? LastLogin { get; set; }

        [DisplayName("Expiry days")]
        public int ExpiryDays { get; set; }

        public bool Disabled { get; set; }

        [DisplayName("Display name")]
        public string DisplayName { get; set; }

        [DisplayName("Office code")]
        public string OfficeCode { get; set; }

        public List<BusinessLogic.Entities.UserRole> Roles { get; set; }

        public List<BusinessLogic.Entities.UserFundGroup> FundGroups { get; set; }
    }
}