using LibraryApp.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Loan;

namespace LibraryApp.Controllers;

    public class LoanController : Controller
    {
        private readonly LoanRepository _loanRepository;

        public LoanController(LoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<LoanVm> vmList = _loanRepository.GetAll();
            return View(vmList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var vm = new LoanCreateVm
            {
                LoanDate = DateTime.Today, 
                DueDate = DateTime.Today.AddDays(15) 
            };
        
            ViewBag.Members = _loanRepository.GetActiveMembers();
            ViewBag.Books = _loanRepository.GetAvailableBooks();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoanCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                _loanRepository.Insert(vm);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
    }
