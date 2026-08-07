using AutoMapper;
using Ee.Ebs.Application.Books;
using Ee.Ebs.Application.Loans;
using Ee.Ebs.Application.Loans.DTOs;
using Ee.Ebs.Application.Members;
using Ee.Ebs.Web.Library.ViewModels.Loan;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Library.Controllers;

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

        try
        {
            var dto = _mapper.Map<LoanCreateDto>(vm);
            _loanService.Insert(dto);
        
            return RedirectToAction("Index");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.Members = _memberService.GetAll(); // Kendi servisindeki metot adını yaz
            ViewBag.Books = _bookService.GetAvailableBooks();
            return View(vm);
        }
    }
    
    [HttpGet]
    public IActionResult ReturnBook(int id)
    {
        try
        {
            _loanService.ReturnBook(id);
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
    
}