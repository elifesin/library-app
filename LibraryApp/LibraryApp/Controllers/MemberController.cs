using AutoMapper;
using Domain;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Members;

namespace LibraryApp.Controllers;

public class  MemberController : Controller
{
    private readonly MemberRepository _memberRepository;
    private readonly LoanRepository _loanRepository;
    private readonly IMapper _mapper;

    public MemberController(MemberRepository memberRepository, LoanRepository loanRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _loanRepository = loanRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var memberEntities = _memberRepository.GetAllMembers();
        var memberViewModels = _mapper.Map<List<MemberVm>>(memberEntities);
        
        return View(memberViewModels);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View(new MemberCreateVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MemberCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var memberEntity = _mapper.Map<Member>(vm);
        _memberRepository.Insert(memberEntity);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm =  _memberRepository.GetMemberById(id);
        if (vm == null)
        {
            return NotFound();
        }
        var bookEntity = _mapper.Map<MemberVm>(vm);
        return View(bookEntity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(MemberVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var memberEntity = _mapper.Map<Member>(vm);
        _memberRepository.Update(memberEntity);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var vm = _memberRepository.GetMemberById(id);
        
        if (vm == null)
        {
            return NotFound();
        }
        
        var bookEntity = _mapper.Map<MemberVm>(vm);
        
        return View(bookEntity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _memberRepository.Delete(id);
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult BorrowedBooks(int id) 
    {
        var member = _memberRepository.GetMemberById(id);
        if (member == null)
        {
            return NotFound();
        }

        var loans = _loanRepository.GetLoansByMemberId(id);

        var vm = new MemberBorrowedBooksVm
        {
            MemberId = member.ID,
            FullName = member.FullName, 
            
            BorrowedBooks = _mapper.Map<List<BorrowedBookItem>>(loans) 
        };
        return View(vm);
    }
   
}