using Microsoft.AspNetCore.Mvc;
using WebEmployee.web.Services;

namespace WebEmployee.web.Controllers
{
    [Route("api/location")]
    public class LocationController : Controller
    {
        private readonly CountryStateCityService _countryStateCityService;

        public LocationController(CountryStateCityService countryStateCityService)
        {
            _countryStateCityService = countryStateCityService;
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _countryStateCityService.GetCountriesAsync();
            return Ok(result);
        }

        [HttpGet("states/{countryIso}")]
        public async Task<IActionResult> GetStates(string countryIso)
        {
            var result = await _countryStateCityService.GetStatesAsync(countryIso);
            return Ok(result);
        }

        [HttpGet("cities/{countryIso}/{stateIso}")]
        public async Task<IActionResult> GetCities(string countryIso, string stateIso)
        {
            var result = await _countryStateCityService.GetCitiesAsync(countryIso, stateIso);
            return Ok(result);
        }
    }
}
