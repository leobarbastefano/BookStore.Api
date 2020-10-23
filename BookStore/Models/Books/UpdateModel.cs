namespace BookStore.Api.Models.Books
{
  public class UpdateModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
    }
}