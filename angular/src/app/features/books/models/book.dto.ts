export interface BookDto {
    id: number,
    title: string,
    publishYear: number,
    isBorrowed: boolean,
    authorName: string,
    categoryName: string,
    pubsliherName: string,
}

export interface BookCreateDto{
    title: string,
    publishYear: string,
    isBorrowed: boolean,
    authorName: string,
    categoryName: string,
    pubsliherName: string,
}

export interface BookEditDto{
    id: number,
    title: string,
    publishYear: number,
    authorName: string,
    categoryName: string,
    publisherName: string,
    isBorrowed: boolean,
}