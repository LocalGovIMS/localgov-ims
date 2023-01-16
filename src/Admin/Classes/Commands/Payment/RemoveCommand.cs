using Admin.Models.Payment;
using Admin.Models.Shared;
using log4net;
using System;
using System.Linq;
using System.Web;

namespace Admin.Classes.Commands.Payment
{
    public class RemoveCommand : BaseCommand<Guid>
    {
        public RemoveCommand(ILog log) : base(log)
        {
        }

        protected override CommandResult OnExecute(Guid id)
        {
            var model = HttpContext.Current.Session["PaymentModel"] as IndexViewModel;

            if (IsValid(model, id))
            {
                model.Basket.RemoveItem(id);

                // The item we're removing could be the item used to set the address. So we need to remove the address now.
                model.Address = null;

                HttpContext.Current.Session["PaymentModel"] = model;

                return new CommandResult(true) { Data = model };
            }
            else
            {
                return new CommandResult(false) { Data = model };
            }
        }

        private bool IsValid(IndexViewModel model, Guid id)
        {
            if (model.Basket == null)
            {
                model.Message = new ErrorMessage("The account reference to delete does not exists in the basket");
                return false;
            }

            if (!model.Basket.Items.Any(x => x.Id == id))
            {
                model.Message = new ErrorMessage("The account reference to delete does not exists in the basket");
                return false;
            }

            model.Message = null;
            return true;
        }
    }
}