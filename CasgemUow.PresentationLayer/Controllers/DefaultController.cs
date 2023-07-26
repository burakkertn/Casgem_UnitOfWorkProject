using CasgemUow.BusinessLayer.Abstract;
using CasgemUow.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CasgemUow.PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerProcessService _customerProcessService;


        public DefaultController(ICustomerService customerService, ICustomerProcessService customerProcessService)
        {
            _customerService = customerService;
            _customerProcessService = customerProcessService;
        }

        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Index(CustomerProcess customerProcess)
        {
            var valueReciver = _customerService.TGetByID(customerProcess.SenderID);
            var valueSender = _customerService.TGetByID(customerProcess.ReceiverID);

            valueReciver.CustomerBalance += customerProcess.Amount;
            valueSender.CustomerBalance -= customerProcess.Amount;

            List<Customer> modifiedCustomer = new List<Customer>()

            {
                valueSender,
                     valueReciver

            };
            _customerService.TMultiUpdate(modifiedCustomer);
            return RedirectToAction("CustomerProcessList");
        }
    }
}
