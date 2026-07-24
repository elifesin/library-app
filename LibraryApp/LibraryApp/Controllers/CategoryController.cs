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
            string sqlQuery = "SELECT * FROM Categories";
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
    
    
}