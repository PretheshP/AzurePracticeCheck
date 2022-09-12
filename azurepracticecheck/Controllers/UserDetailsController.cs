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
    public class UserDetailsController : ControllerBase
    {
        //login,sigin
        [HttpGet]
        public string Get(string username, string password)
        {
            List<UserDetails> list = MovieOperations.UserList();
            bool user = list.Any(p => p.UserName == username && p.Password == password);
            if (user == true)
                return "true";
            return "falseSubmission";
        }
        //signup
        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] UserDetails user)
        {
            MovieOperations.Insert(user);
            return Ok();
        }
    }
}
