namespace Case.Dtos
{
    public class ListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsShopping { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<ListItemDto> Items { get; set; }
    }

    public class ListItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Note { get; set; }
        public bool IsBought { get; set; }
    }
}
