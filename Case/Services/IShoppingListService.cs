using System.Collections.Generic;
using System.Threading.Tasks;
using Case.Dtos;

namespace Case.Services
{
    public interface IShoppingListService
    {
        Task<IEnumerable<ListDto>> GetAllLists();
        Task<ListDto> GetListById(int id);
        Task<bool> CreateList(ListDto listDto);
        Task<bool> UpdateList(ListDto listDto);
        Task<bool> DeleteList(int id);
        Task<bool> AddProductToList(int listId, int productId, string note);
        Task<bool> RemoveProductFromList(int listId, int productId);
        Task<bool> MarkProductAsBought(int listId, int productId);
        Task<bool> MarkListAsShopping(int listId);
        Task<bool> MarkListAsCompleted(int listId);
    }
}
