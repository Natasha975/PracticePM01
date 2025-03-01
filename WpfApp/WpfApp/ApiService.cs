using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp
{
	public class ApiService
	{
		private readonly HttpClient _httpClient;
		private const string BaseUrl = "http://.../api/";

		public ApiService()
		{
			_httpClient = new HttpClient();
		}

		public async Task<AuthResponse> AuthenticateAsync(string login, string password)
		{
			var userLogin = new UserLogin { Логин = login, Пароль = password };
			var json = JsonSerializer.Serialize(userLogin);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync($"{BaseUrl}Пользователь", content);
			response.EnsureSuccessStatusCode();

			var responseJson = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<AuthResponse>(responseJson);
		}
	}
}
