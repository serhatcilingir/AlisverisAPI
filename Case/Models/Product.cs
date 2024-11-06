using Case.Models;

namespace Case.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
