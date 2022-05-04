using FoodTruckAPI.ClassLibrary.DataAccess;
using FoodTruckAPI.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodTruckAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
   

        private readonly ILogger<MenuItemController> _logger;
        private readonly FoodTruckContext _ft;

        public MenuItemController(ILogger<MenuItemController> logger, FoodTruckContext ft)
        {
            _logger = logger;
            _ft = ft;
        }

        [HttpPost]
        public async Task<ActionResult<MenuItem>> Post(MenuItem menuItem)
        {
            try
            {
                await _ft.AddRangeAsync(menuItem);
                await _ft.SaveChangesAsync();
                return menuItem;
            }
            catch { return new ContentResult() { StatusCode = 500 }; }

        }
        [HttpGet("all")]
        public async Task<ActionResult<List<MenuItem>>> Get()
        {
            return Ok(await _ft.MenuItems.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> Get(int id)
        {
            var menuItem1 = await _ft.MenuItems.FindAsync(id);
            if (menuItem1 == null)
            { return BadRequest("Menu item not found."); }
            else
            { return Ok(menuItem1); }
        }
            

        [HttpGet("name")]
        public async Task<ActionResult<MenuItem>> GetName(string Name)
        {
            var menuItem1 = _ft.MenuItems
                        .Where(b => b.Name == Name)
                        .FirstOrDefault<MenuItem>();
            if (menuItem1 == null)
                return BadRequest("menuItem not found.");
            return Ok(menuItem1);
        }

        [HttpGet("foodtype")]
        public async Task<ActionResult<MenuItem>> GetFoodType(string FoodType)
        {

            var menuItem1 = _ft.MenuItems
                        .Where(b => b.FoodType == FoodType);
                       
            if (menuItem1 == null)
                return BadRequest("menuItem not found.");
            return Ok(menuItem1.ToList());
        }

        // Won't be needed due to set up in angular currently
        [HttpPut("priceChangeName")]
        public async Task<ContentResult> UpdatePriceByName(MenuItem menuItem)
        {
            var menuItem1 = _ft.MenuItems.
                            First(b => b.Name == menuItem.Name);

            menuItem1.Price = menuItem.Price;
            _ft.SaveChanges();

            return new ContentResult() { StatusCode = 200 };
        }

        [HttpPut("priceChangeID")]
        public async Task<ContentResult> UpdatePriceByID(MenuItem menuItem)
        {
            var menuItem1 = _ft.MenuItems.
                            First(b => b.MenuItemID == menuItem.MenuItemID);

            menuItem1.Price = menuItem.Price;
            _ft.SaveChanges();

            return new ContentResult() { StatusCode = 200 };
        }
        [HttpDelete("{id}")]
        public async Task<ContentResult> Delete(int id)
        {
            var menuItem1 = await _ft.MenuItems.FindAsync(id);
            if (menuItem1 == null)
                return new ContentResult()
                {
                    StatusCode = 400,
                    Content="Item not found."
                };

            _ft.MenuItems.Remove(menuItem1);
            await _ft.SaveChangesAsync();
            return new ContentResult()
            {
                StatusCode = 200,
            };
        }

    }
}