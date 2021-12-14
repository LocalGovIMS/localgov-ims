﻿using Admin.Models.MethodOfPaymentMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IMethodOfPaymentMetadataService _service;

        public DetailsViewModelBuilder(ILog log
            , IMethodOfPaymentMetadataService methodOfPaymentMetadataService)
            : base(log)
        {
            _service = methodOfPaymentMetadataService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _service.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                Key = data.Key,
                Value = data.Value,
                Description = _service.GetMetadata().FirstOrDefault(y => y.Key == data.Key)?.Description ?? "Unknown",
            };
        }
    }
}