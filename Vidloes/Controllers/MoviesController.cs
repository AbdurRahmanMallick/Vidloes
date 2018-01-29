using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidloes.Models;
using Vidloes.ViewModels;
using System.Data.Entity.Validation;

namespace Vidloes.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        private VidlyoesContext context;

        public MoviesController()
        {
            context = new VidlyoesContext();
        }

        protected override void Dispose(bool disposing)
        {
            // base.Dispose(disposing);
            context.Dispose();
        }


        public ActionResult New()
        {
            var genre = context.Genre.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genre
            };

            return View("MovieForm",viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Save(Movie movie)
         {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = context.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;

                context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }
            try
            {
                context.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
         {
             var movie = context.Movies.SingleOrDefault(c => c.Id == id);
 
             if (movie == null)
                 return HttpNotFound();
 
            var viewModel = new MovieFormViewModel
             {
                 Movie = movie,
                 Genres = context.Genre.ToList()
             };
 
             return View("MovieForm", viewModel);
         }
 
 

        public ActionResult Index()
        {
            var movie = context.Movies.Include(c => c.Genre).ToList();
            return View(movie);
        }

        public ActionResult Details(int id)
        {
            var movie = context.Movies.Include(x => x.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
    }
}