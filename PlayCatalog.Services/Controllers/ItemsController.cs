using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayCatalog.Services.DataTransferOjects;
using PlayCatalog.Services.Entities;
using PlayCatalog.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PlayCatalog.Services.DataTransferOjects.DTOs;

namespace PlayCatalog.Services.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        /* public static readonly List<ItemDTO> items = new List<ItemDTO>()
         {
             new ItemDTO(Guid.NewGuid(),"Potion","Restores a small amount of HP",5,DateTimeOffset.UtcNow),
             new ItemDTO(Guid.NewGuid(),"Antidote","Cure poison",7,DateTimeOffset.UtcNow),
             new ItemDTO(Guid.NewGuid(),"Bronze Sword","Deal a small amount of Damage",25,DateTimeOffset.UtcNow),
         };*/
        private readonly ItemsRepository itemRepo = new ItemsRepository();

        public ItemsController() { }


        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> Get()
        {
            var items = (await itemRepo.GetItemsAsync()).Select(item => item.AsDTO());
            return items;
        }

        [HttpGet("{id}",Name ="GetItemById")]
        public async Task<ActionResult<ItemDTO>> GetItemByID(Guid id)
        {
            var item = await itemRepo.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item.AsDTO();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem(CreateItemDTO createItem)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = new Item
            {
                Name = createItem.Name,
                Description = createItem.Description,
                Price = createItem.price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await itemRepo.CreateItem(item);
            return CreatedAtRoute("GetItemById", new { id = item.Id }, item);  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, UpdateItemDTO item)
        {
            var itemToUpdate = await itemRepo.GetItemAsync(id);
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Name = item.Name;
            itemToUpdate.Description = item.Description;
            itemToUpdate.Price = item.price;

            await itemRepo.UpdateItem(itemToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await itemRepo.GetItemAsync(id);
            if(item == null)
            {
                return NotFound();
            }
            await itemRepo.RemoveItem(item.Id);
            return NoContent();
        }
    }
}
