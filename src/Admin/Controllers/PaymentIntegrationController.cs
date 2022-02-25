using Admin.Models.PaymentIntegration;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class PaymentIntegrationController : BaseController<IPaymentIntegrationControllerDependencies>
    {
        public PaymentIntegrationController(IPaymentIntegrationControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Payment Integrations")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Payment Integration Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Payment Integration")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            return BaseEdit(model, Dependencies.EditCommand);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Payment Integration")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new EditViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }
    }
}