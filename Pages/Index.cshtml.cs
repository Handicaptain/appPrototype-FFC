using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace appPrototype_FFC.Pages
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string? ErrorMessage { get; set; }

        private readonly IConfiguration _configuration;

        public loginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            // Clear error message on page load
            ErrorMessage = null;
        }

        private bool ValidateUser(string username, string password)
        {
            string connectionString = "Server=localhost;Database=dbfridayfilmclub;User=root;Password=saroot;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database connected successfully.");

                    string query = "SELECT COUNT(*) FROM tblLogin WHERE username = @username AND passwd = @passwd";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@passwd", password);

                        Console.WriteLine($"Executing query: {query} with username = {username} and password = {password}");

                        int userCount = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"User count: {userCount}");

                        return userCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                ErrorMessage = "An error occurred while connecting to the database.";
                return false;
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username and Password are required.";
                return Page();
            }

            // Validate credentials using the database
            if (ValidateUser(Username, Password))
            {
                return RedirectToPage("/Home");
            }

            // If credentials are incorrect, set error message
            ErrorMessage = "Invalid username or password.";
            return Page();
        }

       
    }

    }
