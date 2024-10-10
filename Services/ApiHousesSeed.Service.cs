using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using respTest1.Contexts;
using respTest1.Models;

namespace respTest1.Services
{


	public class ApiHousesSeed
	{
		private static readonly HttpClient client = new HttpClient();

		public async Task<List<Houses>> GetHousesFromApiAsync(string apiUrl)
		{
			var response = await client.GetStringAsync(apiUrl);

			var jsonOptions = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			return JsonSerializer.Deserialize<List<Houses>>(response, jsonOptions);
		}

		public async Task PopulateDatabaseWithApiDataAsync(AppDbContext context, string apiUrl)
		{

			if (await context.Houses.AnyAsync())
			{
				Console.WriteLine("O banco de dados já possui dados, não será nece ssário popular novamente.");
				return;  // Se já existirem dados, não prossegue com a inserção
			}


			var houses = await GetHousesFromApiAsync(apiUrl);

			context.Houses.AddRange(houses);
			await context.SaveChangesAsync();  // Salva as alterações no banco de dados

		}
	}
}