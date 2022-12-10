using Microsoft.AspNetCore.Mvc;
using OmahaDotDev.Model.Common;
using OmahaDotDev.Model.Common.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OmahaDotDev.WebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Throw : ControllerBase
    {
        // GET: api/<Throw>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new ForbiddenException(new AmbientContext(), "Sample exception.");
            return new string[] { "value1", "value2" };
        }

        //    // GET api/<Throw>/5
        //    [HttpGet("{id}")]
        //    public string Get(int id)
        //    {
        //        return "value";
        //    }

        //    // POST api/<Throw>
        //    [HttpPost]
        //    public void Post([FromBody] string value)
        //    {
        //    }

        //    // PUT api/<Throw>/5
        //    [HttpPut("{id}")]
        //    public void Put(int id, [FromBody] string value)
        //    {
        //    }

        //    // DELETE api/<Throw>/5
        //    [HttpDelete("{id}")]
        //    public void Delete(int id)
        //    {
        //    }
        //}
    }
}
