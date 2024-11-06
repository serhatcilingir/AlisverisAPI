using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Case.Models;
using Case.Repositories;
using Case.Services;
using Case.Dtos;

namespace Case.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IRepository<ShoppingList> _shoppingListRepository;
        private readonly IRepository<Product> _productRepository;

        public ShoppingListService(IRepository<ShoppingList> shoppingListRepository, IRepository<Product> productRepository)
        {
            _shoppingListRepository = shoppingListRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ListDto>> GetAllLists()
        {
            var lists = await _shoppingListRepository.GetAllAsync();
            var listDtos = new List<ListDto>();

            foreach (var list in lists)
            {
                var listDto = new ListDto
                {
                    Id = list.Id,
                    Name = list.Name,
                    IsShopping = list.IsShopping,
                    IsCompleted = list.IsCompleted,
                    UserId = list.UserId,
                    UserName = list.User.FirstName + " " + list.User.LastName,
                    Items = new List<ListItemDto>()
                };

                foreach (var item in list.Items)
                {
                    listDto.Items.Add(new ListItemDto
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        ProductName = item.Product.Name,
                        Note = item.Note,
                        IsBought = item.IsBought
                    });
                }

                listDtos.Add(listDto);
            }

            return listDtos;
        }

        public async Task<ListDto> GetListById(int id)
        {
            var list = await _shoppingListRepository.GetByIdAsync(id);
            if (list == null)
            {
                return null;
            }

            var listDto = new ListDto
            {
                Id = list.Id,
                Name = list.Name,
                IsShopping = list.IsShopping,
                IsCompleted = list.IsCompleted,
                UserId = list.UserId,
                UserName = list.User.FirstName + " " + list.User.LastName,
                Items = new List<ListItemDto>()
            };

            foreach (var item in list.Items)
            {
                listDto.Items.Add(new ListItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Note = item.Note,
                    IsBought = item.IsBought
                });
            }

            return listDto;
        }

        public async Task<bool> CreateList(ListDto listDto)
        {
            var list = new ShoppingList
            {
                Name = listDto.Name,
                IsShopping = false,
                IsCompleted = false,
                UserId = listDto.UserId,
                Items = new List<ShoppingListItem>()
            };

            await _shoppingListRepository.AddAsync(list);
            var changes = await _shoppingListRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0; // Operatörü burada kullanın
        }

        public async Task<bool> UpdateList(ListDto listDto)
        {
            var list = await _shoppingListRepository.GetByIdAsync(listDto.Id);
            if (list == null)
            {
                return false;
            }

            list.Name = listDto.Name;
            list.IsShopping = listDto.IsShopping;
            list.IsCompleted = listDto.IsCompleted;

            _shoppingListRepository.Update(list);
            var changes = await _shoppingListRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0; // Operatörü burada kullanın
        }

        public async Task<bool> DeleteList(int id)
        {
            var list = await _shoppingListRepository.GetByIdAsync(id);
            if (list == null)
            {
                return false;
            }

            _shoppingListRepository.Delete(list);
            var changes = await _shoppingListRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0; // Operatörü burada kullanın
        }

        public async Task<bool> AddProductToList(int listId, int productId, string note)
        {
            var list = await _shoppingListRepository.GetByIdAsync(listId);
            if (list == null)
            {
                return false;
            }

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return false;
            }

            var listItem = new ShoppingListItem
            {
                ProductId = productId,
                ShoppingListId = listId,
                Note = note,
                IsBought = false
            };

            list.Items.Add(listItem);
            _shoppingListRepository.Update(list);
            var changes = await _shoppingListRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0; // Operatörü burada kullanın
        }

        public async Task<bool> RemoveProductFromList(int listId, int productId)
        {
            var list = await _shoppingListRepository.GetByIdAsync(listId);
            if (list == null)
            {
                return false;
            }

            var listItem = list.Items.FirstOrDefault(i => i.ProductId == productId);
            if (listItem == null)
            {
                return false;
            }

            list.Items.Remove(listItem);
            _shoppingListRepository.Update(list);
            var changes = await _shoppingListRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0; // Operatörü burada kullanın
        }

        public async Task<bool> MarkProductAsBought(int listId, int productId)
        {
            var list = await _shoppingListRepository.GetByIdAsync(listId);
            if (list == null)
            {
                return false;
            }

            var listItem = list.Items.FirstOrDefault(i => i.ProductId == productId);
            if (listItem == null)
            {
                return false;
            }

            listItem.IsBought = true;
            _shoppingListRepository.Update(list);
            var changes = await _shoppingListRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0; // Operatörü burada kullanın
        }

        public async Task<bool> MarkListAsShopping(int listId)
        {
            var list = await _shoppingListRepository.GetByIdAsync(listId);
            if (list == null)
            {
                return false;
            }

            list.IsShopping = true;
            _shoppingListRepository.Update(list);
            var changes = await _shoppingListRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0; // Operatörü burada kullanın
        }

        public async Task<bool> MarkListAsCompleted(int listId)
        {
            var list = await _shoppingListRepository.GetByIdAsync(listId);
            if (list == null)
            {
                return false;
            }

            list.IsCompleted = true;
            list.IsShopping = false;
            var itemsToRemove = list.Items.Where(i => i.IsBought).ToList();
            foreach (var item in itemsToRemove)
            {
                list.Items.Remove(item);
            }

            _shoppingListRepository.Update(list);
            var changes = await _shoppingListRepository.SaveChangesAsync(); // changes değişkenine atayın
            return changes > 0; // Operatörü burada kullanın
        }
    }
}
