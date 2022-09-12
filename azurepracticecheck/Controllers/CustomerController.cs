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
    public class CustomerController : ControllerBase
    {
       

        //Active & DOL filter view
        [HttpGet]
        public IEnumerable<MovieList> Get()
        {
            DateTime dt = DateTime.Now;
            return MovieOperations.GetConnection().Where(p => p.Active == true && p.DateOfLaunch <= dt);

        }

        // GET: api/Customer/5
        [HttpGet("{userid}", Name = "Get Customer")]
        public object Get(int userid)
        {
            int movieCount = 0;
            List<MovieList> list = new List<MovieList>(MovieOperations.FavoriteList(userid, ref movieCount));

            return new { list, movieCount };
        }

        // POST: api/Customer to add fav movie
        [HttpPost]
        public IActionResult Post([FromBody] List<Favorites> fav)
        {
            MovieOperations.InsertIntoFavorites(fav);
            return Ok();
        }

        


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{favId}")]
        public string Delete(int favId)
        {
            return MovieOperations.Delete(favId);
        }
    }
}
