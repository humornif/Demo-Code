using demo.DBContext;
using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        [HttpPost]
        [Route("savedata")]
        public ActionResult<string> saveData(DemoDto data)
        {
            DemoDBContext dc = new DemoDBContext();
            bool result = dc.saveData(data).GetAwaiter().GetResult();

            if(result)
                return "OK";
            return "ERROR";
        }

        [HttpGet]
        [Route("getdata")]
        public IEnumerable<DemoDto> getData()
        {
            DemoDBContext dc = new DemoDBContext();
            return dc.getAllData().GetAwaiter().GetResult();
        }
    }
}
