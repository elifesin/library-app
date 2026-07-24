using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using LibraryApp.Models.Loan;
using LibraryApp.Models.Book;
using LibraryApp.Models.Author;
using LibraryApp.Models.Members;

namespace LibraryApp.Controllers
{
    public class LoanController : Controller
    {
        private readonly string _connectionString;

        public LoanController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<LoanVm> vmList = new List<LoanVm>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"SELECT l.Id AS Id, b.Title AS BookName, a.FullName AS AuthorName, l.LoanDate AS AlişTarihi 
                FROM Books b INNER JOIN Authors a ON b.AuthorID = a.Id
                INNER JOIN Loans l ON b.Id = l.BookID
                INNER JOIN Members m ON l.MemberID = m.Id";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LoanVm loanVm = new LoanVm();
                            loanVm.Id = Convert.ToInt32(reader["Id"]);
                            loanVm.BookName = reader["BookName"].ToString()!;
                            loanVm.BookAuthor = reader["AuthorName"].ToString()!;
                            loanVm.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
                            vmList.Add(loanVm);
                        }
                    }
                }
            }
            return View(vmList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var vm = new LoanCreateVm
            {
                LoanDate = DateTime.Today, 
                DueDate = DateTime.Today.AddDays(14) 
            };            List<SelectListItem> activeMembers = new List<SelectListItem>();
            List<SelectListItem> availableBooks = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string memberQuery = "SELECT ID, FirstName, LastName FROM Members WHERE IsActive = 1";
                using (SqlCommand memberCmd = new SqlCommand(memberQuery, connection))
                {
                    using (SqlDataReader reader = memberCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            activeMembers.Add(new SelectListItem
                            {
                                Value = reader["ID"].ToString(),
                                Text = reader["FirstName"].ToString() + " " + reader["LastName"].ToString()
                            });
                        }
                    }
                }

                string bookQuery = @"
                    SELECT Id, Title 
                    FROM Books 
                    WHERE IsActive = 1 
                    AND Id NOT IN (SELECT BookID FROM Loans WHERE ReturnDate IS NULL)";

                using (SqlCommand bookCmd = new SqlCommand(bookQuery, connection))
                {
                    using (SqlDataReader reader = bookCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            availableBooks.Add(new SelectListItem
                            {
                                Value = reader["Id"].ToString(),
                                Text = reader["Title"].ToString()
                            });
                        }
                    }
                }
            }

            ViewBag.Members = activeMembers;
            ViewBag.Books = availableBooks;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoanCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sqlQuery =
                        "INSERT INTO Loans (BookID, MemberID, LoanDate, DueDate) VALUES (@BookID, @MemberID, @LoanDate, @DueDate)";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@BookID", vm.BookID);
                        command.Parameters.AddWithValue("@MemberID", vm.MemberID);
                        command.Parameters.AddWithValue("@LoanDate", vm.LoanDate);
                        command.Parameters.AddWithValue("@DueDate", vm.DueDate);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }
    }
}