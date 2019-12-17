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
    public class TestsController : ControllerBase
    {
        private readonly ILogger<TestsController> _logger;

        public TestsController(ILogger<TestsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Test> Get()
        {
            var testDummy = new Test(){
                TestName = "Digital Input 1 Hi",
                TestCode = "DIN1HI",
                LowerLimit = "PASS",
                UpperLimit = "PASS",
                Result = "",
                TestStatus = "",
                TestValue = "",
                TestValueType = TestValueType.PassFail

            };
            var testDummy2 = new Test(){
                TestName = "Digital Input 2 Hi",
                TestCode = "DIN2HI",
                LowerLimit = "PASS",
                UpperLimit = "PASS",
                Result = "",
                TestStatus = "",
                TestValue = "",
                TestValueType = TestValueType.PassFail
            };
            var testDummy3 = new Test(){
                TestName = "Analog Input 1, 0-5V",
                TestCode = "AIN105V",
                LowerLimit = "4.5",
                UpperLimit = "4.7",
                Result = "",
                TestStatus = "",
                TestValue = "",
                TestValueType = TestValueType.Double
            };

            var testList = new List<Test>{
            };
            testList.Add(testDummy);
            testList.Add(testDummy2);
            testList.Add(testDummy3);
            return testList;
        }
    }
}
