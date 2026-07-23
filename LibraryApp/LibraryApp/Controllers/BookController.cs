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
            string sqlQuery = @"
            SELECT b.Id, b.Title, b.PublishYear, 
                   a.FirstName + ' ' + a.LastName AS FullName
            FROM Books b
            INNER JOIN Authors a ON b.AuthorID = a.Id WHERE b.IsActive = 1";

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
                    
                        // JOIN sorgusu sayesinde AuthorFullName artık hata vermeden okunacak
                        bookVm.FullName = reader["FullName"].ToString()!;
                    
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
            string sql = "SELECT * FROM Authors";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                authors.Add(new AuthorVm
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"]
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
                string sqlQuery =
                    "INSERT INTO Books(Title, PublishYear, AuthorID) VALUES (@Title, @PublishYear, @AuthorID)";

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

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm = new BookEditVm();
        List<AuthorVm> authors = new List<AuthorVm>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sqlQuery = "SELECT * FROM Books WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vm.Id = Convert.ToInt32(reader["Id"]);
                        vm.Title = reader["Title"].ToString()!;
                        vm.PublishYear = Convert.ToInt32(reader["PublishYear"]);
                        vm.AuthorID = Convert.ToInt32(reader["AuthorID"]); 
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }

            // BookController.cs -> Edit(GET) metodu içindeki 2. SQL sorgusu (Yazarları çeken kısım)

            string sql = "SELECT * FROM Authors";
            using (SqlCommand command2 = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader2 = command2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        authors.Add(new AuthorVm
                        {
                            Id = (int)reader2["Id"],
                            FirstName = reader2["FirstName"].ToString(),
                            LastName = reader2["LastName"].ToString()
                        });
                    }
                }
            }
            
            ViewBag.Authors = authors;
            return View(vm);
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookEditVm vm)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Books SET Title = @Title, PublishYear = @PublishYear WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", vm.Title);
                    command.Parameters.AddWithValue("@PublishYear", vm.PublishYear);
                    command.Parameters.AddWithValue("@Id", vm.Id);
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var vm = new BookDeleteVm();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Books WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vm.Id = Convert.ToInt32(reader["Id"]);
                        vm.Title = reader["Title"].ToString()!;
                        
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "UPDATE Books SET IsActive = 0 WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        return RedirectToAction(nameof(Index));
    }
}