using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebEmployee.web.Models;
using WebEmployee.web.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            
                if (HelperModel.Countries.Count > 0)
                {
                    HelperModel.Countries = new List<Country>();
                }
            
            var countries = JsonConvert.DeserializeObject<List<Country>>(result);
            HelperModel.Countries.AddRange(countries);
            return Ok(result);
        }

        [HttpGet("states/{countryIso}")]
        public async Task<IActionResult> GetStates(string countryIso)
        {
            var result = await _countryStateCityService.GetStatesAsync(countryIso);
            
                if (HelperModel.States.Count > 0)
                {
                    HelperModel.States = new List<State>();
                }
            
            var states = JsonConvert.DeserializeObject<List<State>>(result);
            HelperModel.States.AddRange(states);
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
