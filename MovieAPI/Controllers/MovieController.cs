using Common.Models;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Repositories;
using System;
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

        [HttpGet("{id}")]
        public Movie GetMovieById(Guid id)
        {
            var result = _movieRepository.GetRecordById(id);
            return result;
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            movie.LastChangedAt = DateTime.UtcNow;
            var result = _movieRepository.InsertRecord(movie);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Upsert(Movie movie)
        {
            if (movie.Id == Guid.Empty)
            {
                return BadRequest("Empty Id");
            }

            movie.LastChangedAt = DateTime.UtcNow;
            _movieRepository.UpsertRecord(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var movie = _movieRepository.GetRecordById(id);

            if (movie == null)
            {
                return BadRequest("Movie does not exist in the Database!");
            }

            _movieRepository.DeleteRecord(id);

            return Ok("Movie (" + id + ") is deleted!");
        }
    }
}