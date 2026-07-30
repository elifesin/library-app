using AutoMapper;
using Data.Entities;
using Data.Repositories;
using LibraryApp.Models.Loan;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

    public class LoanController : Controller
    {
        private readonly LoanRepository _loanRepository;
        private readonly BookRepository _bookRepository;
        private readonly MemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public LoanController(LoanRepository loanRepository, 
            BookRepository bookRepository, 
            MemberRepository memberRepository,
            IMapper mapper)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var loanEntities = _loanRepository.GetAll();
            var loanViewModels = _mapper.Map<List<LoanVm>>(loanEntities);
            
            return View(loanViewModels);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var vm = new LoanCreateVm
            {
                LoanDate = DateTime.Today, 
                DueDate = DateTime.Today.AddDays(15) 
            };
            
            ViewBag.Members = _memberRepository.GetActiveMembers();
            ViewBag.Books = _bookRepository.GetAvailableBooks();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoanCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            
            var loanEntity = _mapper.Map<Loan>(vm);
            _loanRepository.Insert(loanEntity);
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult ReturnBook(int id)
        {
            _loanRepository.ReturnBook(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
