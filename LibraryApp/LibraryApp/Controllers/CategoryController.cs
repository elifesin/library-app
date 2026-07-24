using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using LibraryApp.Models.Category;


namespace LibraryApp.Controllers;
public class CategoryController : Controller
{
    public readonly string _connectionString;

    public CategoryController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<CategoryVm> vmList = new List<CategoryVm>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Categories WHERE IsActive = 1";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CategoryVm vm = new CategoryVm();
                        vm.Id = Convert.ToInt32(reader["Id"]);
                        vm.CategoryName = reader["CategoryName"].ToString()!;
                        vm.IsActive = Convert.ToBoolean(reader["IsActive"]);

                        vmList.Add(vm);
                    }
                }
            } 
        }
        return View(vmList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Create(CategoryVm vm)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Categories(CategoryName) VALUES (@CategoryName) ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue(@"Id", vm.Id);
                    command.Parameters.AddWithValue(@"CategoryName", vm.CategoryName);
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm = new CategoryVm();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlQuery = "SELECT * FROM Categories WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue(@"Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vm.Id = Convert.ToInt32(reader["Id"]);
                        vm.CategoryName = reader["CategoryName"].ToString()!;
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
    public IActionResult Edit(CategoryVm vm)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Categories SET CategoryName = @CategoryName WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue(@"Id", vm.Id);
                    command.Parameters.AddWithValue(@"CategoryName", vm.CategoryName);
                    
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
        var vm = new CategoryVm();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Categories WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue(@"Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vm.Id = Convert.ToInt32(reader["Id"]);
                        vm.CategoryName = reader["CategoryName"].ToString()!;
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
    public IActionResult DeleteConfirmed(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "UPDATE Categories SET IsActive = 0 WHERE Id = @Id";

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