using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bookstore.Models
{
    public class BookModel
    {
        [Key]
        public Guid BookId { get; set; }
        
        public string? Title { get; set; }
        
        public string? Genre { get; set; }

        public string? Author { get; set; }

        public string? Date { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

    }
}
