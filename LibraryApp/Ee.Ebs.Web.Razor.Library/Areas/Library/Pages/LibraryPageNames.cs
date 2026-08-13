namespace Ee.Ebs.Web.Razor.Library.Areas.Library.Pages;

public class LibraryPageNames
{
    private const string Default = "Library.Pages";
    private const string Authors = $"{Default}.Authors";
    private const string Books = $"{Default}.Books";
    private const string Categories = $"{Default}.Categories";
    private const string Members = $"{Default}.Members";
    private const string Publishers = $"{Default}.Publishers";

    
    public const string AuthorsIndex = $"{Authors}.Index";
    public const string AuthorsCreate = $"{Authors}.Create";
    public const string AuthorsUpdate = $"{Authors}.Update";
    public const string AuthorsDelete = $"{Authors}.Delete";
    
    public const string BooksIndex = $"{Books}.Index";
    public const string BooksCreate = $"{Books}.Create";
    public const string BooksUpdate = $"{Books}.Update";
    public const string BooksDelete = $"{Books}.Delete";
    
    public const string CategoriesIndex = $"{Categories}.Index";
    public const string CategoriesCreate = $"{Categories}.Create";
    public const string CategoriesUpdate = $"{Categories}.Update";
    public const string CategoriesDelete = $"{Categories}.Delete";
    
    
    
}


public class EbsMenuItem
{
    public string Name { get; set; }

    public string Active { get; set; }

    public bool IsNew { get; set; }
    public string Url { get; set; }
}

public class EbsMenu
{
    public string WelcomeMessage { get; set; }
    
    public List<EbsMenuItem> Items { get; set; }
}