using Case.Models;

namespace Case.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
