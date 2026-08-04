using Application.Loans.DTOs;
using AutoMapper;
using Domain.Loans;

namespace Application.Loans;

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

    public LoanDto GetById(int id)
    {
        var loan = _loanRepository.GetLoansByMemberId(id);
        return _mapper.Map<LoanDto>(loan);
    }
    
    public void Insert(LoanCreateDto vm)
    {
        var loanEntity = _mapper.Map<Loan>(vm);
        loanEntity.ReturnDate = null; 
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