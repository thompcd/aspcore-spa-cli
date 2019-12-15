using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestFramework;

namespace SvelteCliSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Test> Get()
        {
            var testDummy = new Test(){
                TestName = "Digital Input 1 Hi",
                TestCode = "DIN1HI",
                LowerLimit = "4.95",
                UpperLimit = "5.05",
                Result = "",
                TestStatus = "",
                TestValue = ""

            };
            var testDummy2 = new Test(){
                TestName = "Digital Input 2 Hi",
                TestCode = "DIN2HI",
                LowerLimit = "500000",
                UpperLimit = "20000000",
                Result = "",
                TestStatus = "",
                TestValue = ""
            };
            var testList = new List<Test>{
            };
            testList.Add(testDummy);
            testList.Add(testDummy2);
            return testList;
        }
    }
}
