using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_Management_System.Models;
using Payment_Management_System.Repositories;
using Payment_Management_System.Services;
using Payment_Management_System.ViewModels;

namespace Payment_Management_System.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IPaymentRepository _repository;

        public PaymentsController(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            var payment = _repository.Get(id);
            if (payment == null) return NotFound();
            ViewBag.Id = id;
            return View(payment);
        }

        [HttpPost]
        public IActionResult UpdatePayment(int id, Payment payment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View("Update", payment);
            }
            var oldPayment = _repository.Get(id);
            _repository.Update(oldPayment,payment);
            _repository.Save();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeletePayment(int id)
        {
            var payment = _repository.Get(id);
            _repository.Delete(payment);
            _repository.Save();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult PostPayment(Payment payment)
        {
            if (!ModelState.IsValid) return View("Add", payment);
            _repository.Insert(payment);
            _repository.Save();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Get(int id)
        {
            var payment = _repository.Get(id);
            if (payment == null) return NotFound();
            return View(payment);
        }

        public IActionResult GetPartialPosition(int id)
        {
            var index = new IndexViewModel { Index = id };
            return PartialView("_PartialPosition", index);
        }
    }
}