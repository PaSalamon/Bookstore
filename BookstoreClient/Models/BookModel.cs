using System.ComponentModel.DataAnnotations;

namespace BookstoreClient.Models
{
    public class BookModel
    {
        public string? BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string? Genre { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public string? Date { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public Decimal Price { get; set; }
    }
}
