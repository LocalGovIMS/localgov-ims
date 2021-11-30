using Admin.Classes.Models;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Payment;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class PaymentControllerDependencies : BaseControllerDependencies, IPaymentControllerDependencies
    {
        public PaymentControllerDependencies(ILog log,
            IModelBuilder<IndexViewModel, IndexViewModel> indexViewModelBuilder,
            [Dependency("Add")] IModelCommand<IndexViewModel> addCommand,
            [Dependency("Remove")] IModelCommand<string> removeCommand,
            [Dependency("EmptyBasket")] IModelCommand<string> emptyBasketCommand,
            [Dependency("CheckAddress")] IModelCommand<IndexViewModel> checkAddressCommand,
            [Dependency("CreatePayments")] IModelCommand<IndexViewModel> createPaymentsCommand,
            [Dependency("SetAddress")] IModelCommand<IndexViewModel> setAddressCommand,
            IModelCommand<ProcessPaymentCommandAgrs> processPaymentCommand)
            : base(log)
        {
            IndexViewModelBuilder = indexViewModelBuilder ?? throw new ArgumentNullException("indexViewModelBuilder");
            AddCommand = addCommand ?? throw new ArgumentNullException("addCommand");
            RemoveCommand = removeCommand ?? throw new ArgumentNullException("removeCommand");
            EmptyBasketCommand = emptyBasketCommand ?? throw new ArgumentNullException("emptyBasketCommand");
            CheckAddressCommand = checkAddressCommand ?? throw new ArgumentNullException("checkAddressCommand");
            CreatePaymentsCommand = createPaymentsCommand ?? throw new ArgumentNullException("createPaymentsCommand");
            SetAddressCommand = setAddressCommand ?? throw new ArgumentNullException("setAddressCommand");
            ProcessPaymentCommand = processPaymentCommand ?? throw new ArgumentNullException("processPaymentCommand");
        }

        public IModelBuilder<IndexViewModel, IndexViewModel> IndexViewModelBuilder { get; private set; }

        public IModelCommand<IndexViewModel> AddCommand { get; private set; }
        public IModelCommand<string> RemoveCommand { get; private set; }
        public IModelCommand<string> EmptyBasketCommand { get; private set; }
        public IModelCommand<IndexViewModel> CheckAddressCommand { get; private set; }
        public IModelCommand<IndexViewModel> CreatePaymentsCommand { get; private set; }
        public IModelCommand<IndexViewModel> SetAddressCommand { get; private set; }
        public IModelCommand<ProcessPaymentCommandAgrs> ProcessPaymentCommand { get; private set; }
    }
}