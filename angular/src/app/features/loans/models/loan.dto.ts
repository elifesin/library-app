export interface LoanDto{
    id: number,
    loanDate: Date,
    returnDate: Date,
    dueDate: Date,
    bookName: string,
    fullName: string,
}

export interface LoanCreateDto{
    loanDate: Date,
    returnDate: Date,
    dueDate: Date,
    bookName: string,
    fullName: string,
}