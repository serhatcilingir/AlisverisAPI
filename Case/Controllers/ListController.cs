using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Case.Dtos;
using Case.Services;

namespace Case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListController : ControllerBase
    {
        private readonly IShoppingListService _listService;

        public ListController(IShoppingListService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLists()
        {
            var lists = await _listService.GetAllLists();
            return Ok(lists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetListById(int id)
        {
            var list = await _listService.GetListById(id);
            return list != null ? Ok(list) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateList(ListDto listDto)
        {
            var result = await _listService.CreateList(listDto);
            return result ? Ok("Liste oluşturuldu") : BadRequest("Liste oluşturulamadı");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, ListDto listDto)
        {
            if (id != listDto.Id)
            {
                return BadRequest("ID uyuşmazlığı");
            }
            var result = await _listService.UpdateList(listDto);
            return result ? Ok("Liste güncellendi") : NotFound("Liste bulunamadı");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var result = await _listService.DeleteList(id);
            return result ? Ok("Liste silindi") : NotFound("Liste bulunamadı");
        }
    }
}
