using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace azurepracticecheck.Models
{
    public class Favorites
    {
        public int Id { get; set; }
        public int MovieListId { get; set; }
        public int UserId { get; set; }
    }
}
