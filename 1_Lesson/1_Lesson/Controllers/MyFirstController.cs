using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1_Lesson.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyFirstController : ControllerBase
    {
        private readonly List<WeatherForecast> _weatherForecast;

        //public MyFirstController(WeatherForecast weatherForecast)
        //{
        //    _weatherForecast.Append(weatherForecast);
        //}

        [HttpPost]
        public IActionResult Create([FromBody] DateTime inputDate, [FromBody] int inputTemp)
        {

            _weatherForecast.Append(new WeatherForecast() { Date = inputDate, TemperatureC = inputTemp });            
            return Ok();
        }

        [HttpGet]
        public List<int> Read([FromBody] DateTime date1, [FromBody] DateTime date2)
        {
            List<int> listOfTemp = new List<int>();

            for (int i = 0; i < _weatherForecast.Count(); i++)
            {
                if (_weatherForecast[i].Date >= date1 && _weatherForecast[i].Date <= date2)

                    listOfTemp.Add(_weatherForecast[i].TemperatureC);
            }
            return listOfTemp;

        }

        [HttpPut]
        public IActionResult Update([FromBody] DateTime inputDate, [FromBody] int changeToTemp)
        {
            for (int i = 0; i < _weatherForecast.ToList().Count(); i++)
            {
                if (_weatherForecast[i].Date == inputDate)

                    _weatherForecast[i].TemperatureC = changeToTemp;
            }
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] DateTime date1, [FromBody] DateTime date2)
        {            
            for (int i = 0; i < _weatherForecast.Count(); i++)
            {
                if (_weatherForecast[i].Date >= date1 && _weatherForecast[i].Date <= date2)

                    _weatherForecast.Remove(_weatherForecast[i]);
            }
            return Ok();
        }
    }
}
