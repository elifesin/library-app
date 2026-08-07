using AutoMapper;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Members;
using Ee.Ebs.Application.Contracts.Members.DTOs;
using Ee.Ebs.Application.Loans;
using Ee.Ebs.Application.Members;
using Ee.Ebs.Web.Library.ViewModels.Members;
using Microsoft.AspNetCore.Mvc;

namespace Ee.Ebs.Web.Library.Controllers;

public class MemberController : Controller
{
    private readonly IMemberService _memberService;
    private readonly ILoanService _loanService;
    private readonly IMapper _mapper;

    public MemberController(IMemberService memberService, IMapper mapper, ILoanService loanService)
    {
        _memberService = memberService;
        _mapper = mapper;
        _loanService = loanService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var memberDtos = _memberService.GetAll();
        var memberViewModels = _mapper.Map<List<MemberVm>>(memberDtos);
        
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
        
        var memberDto = _mapper.Map<MemberCreateDto>(vm);
        _memberService.Insert(memberDto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var memberDto = _memberService.GetById(id);
        if (memberDto == null) return NotFound();
        
        var vm = _mapper.Map<MemberVm>(memberDto);
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(MemberVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        
        var memberDto = _mapper.Map<MemberDto>(vm);
        _memberService.Update(memberDto);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        var memberDto = _memberService.GetById(id);
        if (memberDto == null) return NotFound();
        
        var vm = _mapper.Map<MemberVm>(memberDto);
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _memberService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult BorrowedBooks(int id)
    {
        var loanDtos = _loanService.GetLoansByMemberId(id);
        var memberDto = _memberService.GetById(id);
        
        var viewModel = new MemberBorrowedBooksVm
        {
            FullName = memberDto.FirstName + " " + memberDto.LastName, // veya memberDto.FullName
            BorrowedBooks = _mapper.Map<List<BorrowedBookItem>>(loanDtos) // DTO listesini item listesine çeviriyoruz
        };

        // 3. Modeli View'a gönderiyoruz
        return View(viewModel);
    }
}