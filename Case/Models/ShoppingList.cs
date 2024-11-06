using Case.Models;

namespace Case.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsShopping { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<ShoppingListItem> Items { get; set; }
    }
}
