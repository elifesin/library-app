using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using LibraryApp.Models;

namespace LibraryApp.Controllers;

public class AuthorController : Controller
{
    private readonly string _connectionString;

    public AuthorController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    
    // 
    [HttpGet]
    public IActionResult Index()
    {
        
        List<Authors> authorsList = new List<Authors>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Authors";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Authors author = new Authors();
                        author.Id = Convert.ToInt32(reader["Id"]);
                        author.FirstName = reader["FirstName"].ToString()!;
                        author.LastName = reader["LastName"].ToString()!;
                        
                        authorsList.Add(author);
                    }
                }
            }
        }
        
        return View(authorsList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Authors author)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Authors(FirstName, LastName) VALUES (@FirstName, @LastName)";
                
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", author.FirstName);
                    command.Parameters.AddWithValue("@LastName", author.LastName);

                    connection.Open();
                    command.ExecuteNonQuery(); 
                }
            }
            return RedirectToAction(nameof(Index));
        }
        
        return View(author);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Authors author = new Authors();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Authors WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        author.Id = Convert.ToInt32(reader["Id"]);
                        author.FirstName = reader["FirstName"].ToString()!;
                        author.LastName = reader["LastName"].ToString()!;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Authors author)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Authors SET FirstName = @FirstName, LastName = @Lastname WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@FirstName", author.FirstName);
                    command.Parameters.AddWithValue("@LastName", author.LastName);
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(author);
    }
    
    
}