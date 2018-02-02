using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidloes.Models;
using Vidloes.Dtos;
using System.Data.Entity;
using AutoMapper;

namespace Vidloes.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private VidlyoesContext context;

        public MoviesController()
        {
            context = new VidlyoesContext();
        }

        //Get/api/Movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }


        //Get/api/Movies/1
        public IHttpActionResult GetMovies(int id)
        {
            var movie = context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));

        }

        //post/api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            context.Movies.Add(movie);
            context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }


        //PUT/api/Movies/1
        [HttpPut]
        public void UpdateMovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDB = context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDB == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDB);
            context.SaveChanges();
        }


        //DELETE/api/movie/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            context.Movies.Remove(movieInDb);
            context.SaveChanges();

        }
    }
}
