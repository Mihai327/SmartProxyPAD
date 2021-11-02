using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAplication.Models
{
    public class MovieData : MongoDocument
    {
        public string Name { get; set; }
        public List<string> Actors { get; set; }
        public decimal? Budget { get; set; }
        public string Description { get; set; }
    }
}
