using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using respTest1.Contexts;
using respTest1.Models;

namespace respTest1.Controllers
{
	[ApiController]
	[Route("/api/houses")]
	public class HousesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public HousesController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Houses>>> GetHouses()
		{
			return await _context.Houses.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Houses>> GetHouseById(int id)
		{
			var house = await _context.Houses.FindAsync(id);
			if (house == null)
			{
				return NotFound();
			}
			return Ok(house);
		}
	}
}