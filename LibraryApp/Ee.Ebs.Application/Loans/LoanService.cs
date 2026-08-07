using AutoMapper;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Ee.Ebs.Domain.Loans;

namespace Ee.Ebs.Application.Loans;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IMapper _mapper;

    public LoanService(ILoanRepository loanRepository, IMapper mapper)
    {
        _loanRepository = loanRepository;
        _mapper = mapper;
    }

    public List<LoanDto> GetAll()
    {
        var loans = _loanRepository.GetAll();
        return _mapper.Map<List<LoanDto>>(loans);
    }
    
    public void Insert(LoanCreateDto vm)
    {
        var loanEntity = new Loan(
            bookId: vm.BookID,
            memberId: vm.MemberID,
            loanDate: vm.LoanDate,
            dueDate: vm.DueDate
        );
        _loanRepository.Insert(loanEntity);
        
    }

    public void ReturnBook(int id)
    {
        _loanRepository.ReturnBook(id);
    }
    
    public List<LoanDto> GetLoansByMemberId(int id)
    {
        var loans = _loanRepository.GetLoansByMemberId(id);
        return _mapper.Map<List<LoanDto>>(loans);
    }
}