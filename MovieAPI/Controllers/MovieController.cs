using Common.Models;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Repositories;
using System.Collections.Generic;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMongoRepository<Movie> _movieRepository;
        public MovieController(IMongoRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public List<Movie> GetAllMovies()
        {
            var records = _movieRepository.GetAllRecords();

            return records;
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            _movieRepository.InsertRecord(movie);

            return Ok("Created");
        }
    }
}