namespace WebEmployee.web.Services
{
    public class CountryStateCityService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "TjhRbk1lOVE5RlQ3U3Y0SG9LV0ZWT0l2a2V0RWticjJFaE5aODdNdQ==";
        private const string BaseUrl = "https://api.countrystatecity.in/v1/";

        public CountryStateCityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-CSCAPI-KEY", ApiKey);
        }

        public async Task<string> GetCountriesAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}countries");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetStatesAsync(string countryIso)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}countries/{countryIso}/states");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetCitiesAsync(string countryIso, string stateIso)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}countries/{countryIso}/states/{stateIso}/cities");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
