using LibraryApp.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Members;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Controllers;

public class MemberController : Controller
{
    private readonly MemberRepository _memberRepository;
    private readonly LoanRepository _loanRepository;

    public MemberController(MemberRepository memberRepository, LoanRepository loanRepository)
    {
        _memberRepository = memberRepository;
        _loanRepository = loanRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<MemberVm> vmList = _memberRepository.GetAllMembers();
        return View(vmList);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MemberCreateVm vm)
    {
        if (ModelState.IsValid)
        {
            _memberRepository.Insert(vm);
            return RedirectToAction(nameof(Index));

        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm =  _memberRepository.GetMemberById(id);
        if (vm == null)
        {
            return NotFound();
        }
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(MemberVm vm)
    {
        if (ModelState.IsValid)
        {
            _memberRepository.Update(vm);
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var vm = _memberRepository.GetMemberById(id);
        if (vm == null)
        {
            return NotFound();
        }
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _memberRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult BorrowedBooks(int id) // id = MemberId
    {
        // Önce üyenin adını sayfada göstermek için üye bilgilerini çekiyoruz
        var member = _memberRepository.GetMemberById(id);
        if (member == null)
        {
            return NotFound();
        }

        ViewBag.MemberName = $"{member.FirstName} {member.LastName}";
    
        // Sonra bu üyenin ödünç aldığı kitapların listesini çekiyoruz
        var loans = _loanRepository.GetLoansByMemberId(id);
    
        return View(loans);
    }
   
}