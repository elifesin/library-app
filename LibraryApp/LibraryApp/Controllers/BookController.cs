using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using LibraryApp.Models.Book;
using LibraryApp.Models.Author;


namespace LibraryApp.Controllers;

public class BookController : Controller
{
    private readonly string _connectionString;
    public BookController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<BookVm> vmList = new List<BookVm>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Books";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookVm bookVm = new BookVm();
                        bookVm.Id = Convert.ToInt32(reader["Id"]);
                        bookVm.Title = reader["Title"].ToString()!;
                        bookVm.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                        
                        vmList.Add(bookVm);

                    }
                }
            }
        }
        return View(vmList);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        List<AuthorVm> authors = new List<AuthorVm>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT Id, FirstName + ' ' + LastName AS FullName FROM Authors";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                authors.Add(new AuthorVm
                {
                    Id = (int)reader["Id"],
                    FullName = reader["FullName"].ToString()!
                });
            }
        }

        ViewBag.Authors = authors;
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookCreateVm vm)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Books(Title, PublishYear, AuthorID) VALUES (@Title, @PublishYear, @AuthorID)";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", vm.Title);
                    command.Parameters.AddWithValue("@PublishYear", vm.PublishYear);
                    command.Parameters.AddWithValue("@AuthorID", vm.AuthorID);
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }
    
    public IActionResult Edit()
    
    
}