using AutoMapper;
using Ee.Ebs.Application.Contracts.Loans;
using Ee.Ebs.Application.Contracts.Loans.DTOs;
using Ee.Ebs.Domain.Loans;

namespace Ee.Ebs.Application.Loans;

public class LoanAppService : ILoanAppService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IMapper _mapper;

    public LoanAppService(ILoanRepository loanRepository, IMapper mapper)
    {
        _loanRepository = loanRepository;
        _mapper = mapper;
    }

    public List<LoanDto> GetAll()
    {
        var loans = _loanRepository.GetAll();
        return _mapper.Map<List<LoanDto>>(loans);
    }

    public LoanDto GetById(int id)
    {
        var loan = _loanRepository.GetById(id);
        return _mapper.Map<LoanDto>(loan);
    }
    
    public void Insert(LoanCreateDto vm)
    {
        var loanEntity = new Loan(
            bookId: vm.BookID,
            memberId: vm.MemberID,
            loanDate: vm.LoanDate,
            dueDate: vm.DueDate
        );
        
        bool hasDelayedBook = _loanRepository.GetAll().Any(l => 
            l.MemberID == loanEntity.MemberID &&
            l.ReturnDate == null &&
            l.DueDate < DateTime.Today);
        
        int activeLoanCount = _loanRepository.GetAll().Count(l =>
            l.MemberID == loanEntity.MemberID && l.ReturnDate == null);

        if (activeLoanCount >= 5)
        {
            throw new InvalidOperationException("Aynı anda en fazla 5 kitap ödünç alınabilir!");
        }
        
        _loanRepository.Insert(loanEntity);

        if (hasDelayedBook)
        {
            throw new InvalidOperationException("İade tarihi geçmiş kitabınız olduğu için yeni kitap alamazsınız!");
        }
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