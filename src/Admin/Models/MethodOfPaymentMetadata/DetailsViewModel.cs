﻿using System.ComponentModel;

namespace Admin.Models.MethodOfPaymentMetadata
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Key")]
        public int MopMetadataKeyId { get; set; }

        [DisplayName("Key")]
        public string MopMetadataKeyName { get; set; }

        [DisplayName("Description")]
        public string MopMetadataKeyDescription { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }

        [DisplayName("MOP Code")]
        public string MopCode { get; set; }
    }
}