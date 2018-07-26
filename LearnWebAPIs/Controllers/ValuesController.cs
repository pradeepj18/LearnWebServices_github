using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebAPIs.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static List<string> strlist = new List<string>() {
            "String 1",
            "String 2",
            "String 3",
            "String 4"
        };
        // GET api/values
        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<string> Get()
        {
            return strlist;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery]int id,string text)
        {
            return Ok(new Val { Id = id,Text=text});
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Val value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction("Get", new { id = value.Id, text = value.Text });
           // strlist.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            strlist[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            strlist.RemoveAt(id);
        }
    }
    public class Val
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Text { get; set; }
    }
}
