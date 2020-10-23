using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Models.Books
{
    public class CreateModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}