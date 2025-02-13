using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;

namespace appPrototype_FFC.Pages
{
    public class createLoginModel : PageModel
    {
        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string? ErrorMessage { get; set; }

        private readonly IConfiguration _configuration;

        public createLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            // Clear error message on page load
            ErrorMessage = null;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username and Password are required.";
                return Page();
            }

            string connectionString = "Server=localhost;Database=dbfridayfilmclub;User=root;Password=saroot;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO tblLogin (username, passwd) VALUES (@username, @password)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", Username);
                        command.Parameters.AddWithValue("@password", Password);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Redirect to login page after successful account creation
                            return RedirectToPage("/index");
                        }
                        else
                        {
                            ErrorMessage = "Failed to create account. Please try again.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                ErrorMessage = "An error occurred while connecting to the database.";
            }

            return Page();
        }
    }
}
