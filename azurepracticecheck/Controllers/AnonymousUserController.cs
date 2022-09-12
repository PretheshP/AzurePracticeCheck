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
    public class AnonymousUserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MovieList> Get()
        {
            return MovieOperations.GetConnection();
        }
    }
}
