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
        private readonly ValuesHolder _valuesHolder;        

        public MyFirstController(ValuesHolder valuesHolder)
        {
            _valuesHolder = valuesHolder;            
        }

        [HttpPost]
        public IActionResult Create([FromBody] DateTime inputDate, [FromBody] int inputTemp = 12)
        {
            if (inputDate == null)
            {
                inputDate = DateTime.Now;
            }
            _valuesHolder.WeatherForecasts.Append(new WeatherForecast() { Date = inputDate, TemperatureC = inputTemp });            
            return Ok();
        }

        [HttpGet]
        public List<int> Read([FromBody] DateTime date1, [FromBody] DateTime date2)
        {            
            List<int> listOfTemp = new List<int>();

            for (int i = 0; i < _valuesHolder.WeatherForecasts.Count(); i++)
            {
                if (_valuesHolder.WeatherForecasts[i].Date >= date1 && _valuesHolder.WeatherForecasts[i].Date <= date2)

                    listOfTemp.Add(_valuesHolder.WeatherForecasts[i].TemperatureC);
            }
            return listOfTemp;

        }

        [HttpPut]
        public IActionResult Update([FromBody] DateTime inputDate, [FromBody] int changeToTemp)
        {
            for (int i = 0; i < _valuesHolder.WeatherForecasts.ToList().Count(); i++)
            {
                if (_valuesHolder.WeatherForecasts[i].Date == inputDate)

                    _valuesHolder.WeatherForecasts[i].TemperatureC = changeToTemp;
            }
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] DateTime date1, [FromBody] DateTime date2)
        {            
            for (int i = 0; i < _valuesHolder.WeatherForecasts.Count(); i++)
            {
                if (_valuesHolder.WeatherForecasts[i].Date >= date1 && _valuesHolder.WeatherForecasts[i].Date <= date2)

                    _valuesHolder.WeatherForecasts.Remove(_valuesHolder.WeatherForecasts[i]);
            }
            return Ok();
        }
    }
}
