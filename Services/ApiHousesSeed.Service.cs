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
			if (response == null)
			{
				throw new Exception("Api Seed Error");
			}
			return JsonSerializer.Deserialize<List<Houses>>(response);
		}

		public async Task PopulateDatabaseWithApiDataAsync(AppDbContext context, string apiUrl)
		{

			if (await context.Houses.AnyAsync())
			{
				Console.WriteLine("O banco de dados já possui dados, não será necessário popular novamente.");
				return;  // Se já existirem dados, não prossegue com a inserção
			}


			var houses = await GetHousesFromApiAsync(apiUrl);

			if (houses != null)
			{
				foreach (var house in houses)
				{
					context.Houses.Add(house);  // Adiciona a casa no contexto
				}
				await context.SaveChangesAsync();  // Salva as alterações no banco de dados
			}
		}
	}
}