using Admin.Classes.Models;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Payment;
using System;

namespace Admin.Controllers
{
    public interface IPaymentControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<IndexViewModel, IndexViewModel> IndexViewModelBuilder { get; }

        IModelCommand<IndexViewModel> AddCommand { get; }
        IModelCommand<Guid> RemoveCommand { get; }
        IModelCommand<string> EmptyBasketCommand { get; }
        IModelCommand<IndexViewModel> CheckAddressCommand { get; }
        IModelCommand<IndexViewModel> CreatePaymentsCommand { get; }
        IModelCommand<IndexViewModel> SetAddressCommand { get; }
        IModelCommand<ProcessPaymentCommandAgrs> ProcessPaymentCommand { get; }
    }
}
