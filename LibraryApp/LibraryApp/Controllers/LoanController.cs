using Application.Books;
using Application.Loans;
using Application.Loans.DTOs;
using Application.Members;
using AutoMapper;
using LibraryApp.Models.Loan;
using LibraryApp.Models.Members;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public class LoanController : Controller
{
    private readonly ILoanService _loanService;
    private readonly IBookService _bookService;
    private readonly IMemberService _memberService;
    private readonly IMapper _mapper;

    public LoanController(
        ILoanService loanService, 
        IBookService bookService, 
        IMemberService memberService,
        IMapper mapper)
    {
        _loanService = loanService;
        _bookService = bookService;
        _memberService = memberService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // Ödünç listesini DTO olarak alıp VM'e çeviriyoruz
        var loanDtos = _loanService.GetAll();
        var loanViewModels = _mapper.Map<List<LoanVm>>(loanDtos);
        
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
        
        // Repository yerine artık ilgili servislerden (DTO listeleri) veri çekiyoruz
        ViewBag.Members = _memberService.GetAll();
        ViewBag.Books = _bookService.GetAvailableBooks(); // IBookService'de bu metot varsa kullanıyoruz

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(LoanCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Members = _memberService.GetAll();
            ViewBag.Books = _bookService.GetAvailableBooks();
            return View(vm);
        }
        
        // 1. Ekrandan gelen VM'i DTO'ya çevir
        var loanDto = _mapper.Map<LoanCreateDto>(vm);
        
        // 2. DTO'yu servise gönder (ReturnDate=null gibi iş kuralları serviste halledilecek)
        _loanService.Insert(loanDto);
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult ReturnBook(int id)
    {
        // İade işlemi doğrudan servise paslanıyor
        _loanService.ReturnBook(id);
        
        return RedirectToAction(nameof(Index));
    }
    
}