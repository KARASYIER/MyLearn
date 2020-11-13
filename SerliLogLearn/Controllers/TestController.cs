using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SerliLogLearn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {

            //Serilog.Log.Logger.Error($"出票回填异常： {ex.Message}");

            Serilog.Log.Logger.Error($"Hello world!!!");


            return "1";
        }

    }
}
