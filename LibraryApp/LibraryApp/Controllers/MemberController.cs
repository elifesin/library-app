using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.Members;
using Microsoft.Data.SqlClient;

namespace LibraryApp.Controllers;

public class MemberController : Controller
{
    private readonly string _connectionString;

    public MemberController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<MemberVm> vmList = new List<MemberVm>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Members WHERE IsActive = 1";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MemberVm vm = new MemberVm();
                        vm.ID = Convert.ToInt32(reader["ID"]);
                        vm.FirstName = reader["FirstName"].ToString()!;
                        vm.LastName = reader["LastName"].ToString()!;
                        
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
    public IActionResult Create(MemberCreateVm vm)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Members(FirstName, LastName) VALUES(@FirstName, @LastName)";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", vm.FirstName);
                    command.Parameters.AddWithValue("@LastName", vm.LastName);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
        }
        return View(vm);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var vm = new MemberVm();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sqlQuery = "SELECT * FROM Members  WHERE ID = @ID";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vm.ID = Convert.ToInt32(reader["ID"]);
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
    public IActionResult Edit(MemberVm vm)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Members SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", vm.ID);
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
    
    
   
}