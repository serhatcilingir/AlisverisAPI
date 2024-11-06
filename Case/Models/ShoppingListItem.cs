using Case.Models;

namespace Case.Models
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Note { get; set; }
        public bool IsBought { get; set; }
    }
}
