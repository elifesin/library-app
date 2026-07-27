using LibraryApp.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Members;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Controllers;

public class MemberController : Controller
{
    private readonly MemberRepository _memberRepository;

    public MemberController(MemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
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
   
}