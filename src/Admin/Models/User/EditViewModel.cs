using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.User
{
    public class EditViewModel
    {
        public int UserId { get; set; }

        [DisplayName("User name")]
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [DisplayName("Last login")]
        public DateTime? LastLogin { get; set; }

        [DisplayName("Expiry days")]
        public int ExpiryDays { get; set; }

        public bool Disabled { get; set; }

        [DisplayName("Display name")]
        [MaxLength(150, ErrorMessage = "The Display name can only be 150 characters long")]
        public string DisplayName { get; set; }

        [DisplayName("Office code")]
        [Required(ErrorMessage = "Office code is required")]
        public string OfficeCode { get; set; }

        public SelectList Offices { get; set; }
    }
}