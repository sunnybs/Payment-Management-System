using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_Management_System.Models;
using Payment_Management_System.Repositories;
using Payment_Management_System.ViewModels;


namespace Payment_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPaymentRepository _repository;

        public HomeController(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var payments = _repository.FindAll();
            var viewModel = new PaymentsWithFilterViewModel{Payments = payments};
            return View(viewModel);
        }


        public IActionResult UpdateIndex(PaymentsWithFilterViewModel viewModel)
        {
            var filters = viewModel.Filter.GetFilters();
            var payments = _repository.FindAll(payment => filters.All(filter => filter(payment)));
            return View("Index", new PaymentsWithFilterViewModel { Payments = payments });
        }
        

    }
}
