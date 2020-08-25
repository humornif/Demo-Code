using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace demo.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<string> example_list = null;
                var item_count = example_list.Count();
                return Ok(item_count);
            }
            catch (Exception ex) when (log(ex))
            {
            }
            return StatusCode(HttpContext.Response.StatusCode, "error");
        }

        private bool log(Exception ex)
        {
            Debug.Print(ex.Message);
            return false;
        }
    }
}
