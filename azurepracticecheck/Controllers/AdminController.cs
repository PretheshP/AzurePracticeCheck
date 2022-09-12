using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using azurepracticecheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace azurepracticecheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MovieList> Get()
        {
            return MovieOperations.GetConnection();
        }

        // PUT: api/Admin/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MovieList movie)
        {
            MovieOperations.Update(id, movie);
            return Ok();
        }
        //post: api/Admin
        [HttpPost]
        public IActionResult Post([FromBody] MovieList movie)
        {
            MovieOperations.InsertMov(movie);
            return Ok();
        }
        // DELETE
        [HttpDelete("{MovieId}")]
        public string Deletemov(int MovieId)
        {
            return MovieOperations.Deletemov(MovieId);
        }


    }
}
