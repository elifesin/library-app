using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using LibraryApp.Models.Author;

namespace LibraryApp.Controllers;

public class AuthorController : Controller
{
    private readonly string _connectionString;
    public AuthorController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<AuthorVm> vmList = new List<AuthorVm>();
     
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Authors WHERE Isactive = 1";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AuthorVm authorVm = new AuthorVm();
                        authorVm.Id = Convert.ToInt32(reader["Id"]);
                        authorVm.FirstName = reader["FirstName"].ToString()!;
                        authorVm.LastName = reader["LastName"].ToString()!;
                        
                        vmList.Add(authorVm);
                    }
                }
            }
        }
        return View(vmList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AuthorCreateVm vm)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Authors(FirstName, LastName) VALUES (@FirstName, @LastName)";
                
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", vm.FirstName);
                    command.Parameters.AddWithValue("@LastName", vm.LastName);

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
        var vm = new AuthorEditVm();

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
                        vm.Id = Convert.ToInt32(reader["Id"]);
                        vm.FirstName = reader["FirstName"].ToString()!;
                        vm.LastName = reader["LastName"].ToString()!;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AuthorEditVm vm)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Authors SET FirstName = @FirstName, LastName = @Lastname WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", vm.Id);
                    command.Parameters.AddWithValue("@FirstName", vm.FirstName);
                    command.Parameters.AddWithValue("@LastName", vm.LastName);
                    
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
        var vm = new AuthorVm();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Authors WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command .Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vm.Id = Convert.ToInt32(reader["Id"]);
                        vm.FirstName = reader["FirstName"].ToString();
                        vm.LastName = reader["LastName"].ToString();
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
            string sqlQuery = "UPDATE Authors SET IsActive = 0 WHERE Id = @Id";

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