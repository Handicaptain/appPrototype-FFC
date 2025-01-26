using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace appPrototype_FFC.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<FilmItem> Films;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Films = new List<FilmItem>();

            Films.Add(new FilmItem { 
                Id = 1,
                Title = "Inception",
                Rating = 5,
                ImagePath = "/images/inception.jpg",
                WeeklyPick = true,
                Description = "Inception is a 2010 science fiction action film written and directed by Christopher Nolan, who also produced the film with Emma Thomas, his wife. The film stars Leonardo DiCaprio as a professional thief who steals information by infiltrating the subconscious of his targets. He is offered a chance to have his criminal history erased as payment for the implantation of another person's idea into a target's subconscious.",
                Year = 2010,
                DurationHrs = 2,
                DurationMins = 28
            });
            Films.Add(new FilmItem
            {
                Id = 2,
                Title = "The Matrix",
                Rating = 4,
                ImagePath = "/images/matrix.jpg",
                Description = "The Matrix is a 1999 science fiction action film written and directed by the Wachowskis. It is the first installment in The Matrix film series, starring Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss, Hugo Weaving, and Joe Pantoliano. It depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix, created by intelligent machines to distract humans while using their bodies as an energy source."
                       
            });

        }
    }

    public class FilmItem {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public string ImagePath{ get; set; }
        public string Description { get; set; }
        public bool WeeklyPick { get; set; }
        public int Year { get; set; }
        public int DurationHrs { get; set; }
        public int DurationMins { get; set; }
    }
}
