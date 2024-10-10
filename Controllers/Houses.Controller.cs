using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using respTest1.Contexts;
using respTest1.Models;

namespace respTest1.Controllers
{
	[ApiController]
	[Route("/api/houses")]
	public class HousesController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMemoryCache _cache;

		private const string AllHousesCacheKey = "all_houses";

		public HousesController(AppDbContext context, IMemoryCache cache)
		{
			_context = context;
			_cache = cache;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Houses>>> GetHouses()
		{

			if (_cache.TryGetValue(AllHousesCacheKey, out var housesFromCache))
			{
				Console.WriteLine("House Cache Match");
				Response.Headers.Append("is-cached", "true");
				return Ok(housesFromCache);
			}

			Console.WriteLine("Fetching Houses from database");
			var housesFromDB = await _context.Houses.ToListAsync();
			_cache.Set(AllHousesCacheKey, housesFromDB, TimeSpan.FromMinutes(1));
			return Ok(housesFromDB);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Houses>> GetHouseById(int id)
		{
			string houseCacheKey = $"house_{id}";
			if (_cache.TryGetValue(houseCacheKey, out var houseFromCache))
			{
				Console.WriteLine($"House-{id} Cache Match");
				Response.Headers.Append("is-cached", "true");
				return Ok(houseFromCache);
			}
			Console.WriteLine($"Fetching House-{id} from database");

			var house = await _context.Houses.FindAsync(id);
			if (house == null)
			{
				return NotFound();
			}
			_cache.Set(houseCacheKey, house, TimeSpan.FromMinutes(1));
			return Ok(house);
		}
	}
}