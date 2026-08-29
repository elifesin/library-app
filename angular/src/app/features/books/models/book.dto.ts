export interface BookDto {
  id: number;
  isActive: boolean;
  title: string;
  publishYear: number;
  isBorrowed: boolean;
  authorName: string;
  categoryName: string;
  publisherName: string;
}

export interface BookCreateDto {
  authorID: number;
  title: string;
  publishYear: number;
  categoryID: number; 
  publisherId: number;
  authorFullName: string;
}

export interface BookEditDto {
  id: number;
  authorID: number;
  title: string;
  publishYear: number;
  isBorrowed: boolean;
  publisherId: number;
  categoryID: number;
}