﻿namespace Api.Controllers.FundMetadata
{
    public class FundMetadataModel
    {
        public string FundCode { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public FundMetadataModel(BusinessLogic.Entities.FundMetaData source)
        {
            FundCode = source.FundCode;
            Key = source.Key;
            Value = source.Value;
            
        }
    }
}