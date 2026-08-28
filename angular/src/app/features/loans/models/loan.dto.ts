export interface LoanDto {
  id: number;
  loanDate: string | Date;
  dueDate: string | Date;
  returnDate?: string | Date | null;
  bookName: string;
  memberFullName: string;
}

export interface LoanCreateDto {
  memberID: number;
  bookID: number;
  loanDate: string | Date;
  dueDate: string | Date;
  returnDate?: string | Date | null;
}