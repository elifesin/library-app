export interface AuthorDto {
    id: number;
    firstName: string;
    lastName:string;
    isActive: boolean;
    fullName: string;
}

export interface AuthorCreateDto {
    firstName: string;
    lastName:string;
}

export interface AuthorEditDto {
    id: number;
    firstName: string;
    lastName:string;
}

