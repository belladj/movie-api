using movies_api.Models;
using movies_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace movies_api.Controllers
{
    public class MovieController : Controller
    {
        [HttpGet]
        [Route("Movies")]
        public JsonResult GetMovieList()
        {
            List<MovieModel> result = MovieService.GetMovies();
            return ResultService.SuccessResult<List<MovieModel>>(result);

        }

        [HttpGet]
        [Route("Movies/{id}")]
        public JsonResult GetMovie([System.Web.Http.FromBody] int id)
        {

            MovieModel result = MovieService.GetMovieById(id);
            return ResultService.SuccessResult<MovieModel>(result);

        }

        [HttpPost]
        [Route("Movies")]
        public JsonResult AddMovie([System.Web.Http.FromBody] NewMovieModel model)
        {
            if (string.IsNullOrEmpty(model.rating.ToString())) model.rating = 0;
            if (string.IsNullOrEmpty(model.description)) model.description = "";
            if (string.IsNullOrEmpty(model.image)) model.image = "";

            int result = MovieService.AddMovie(model);
            if (result == 0)
            {
                MovieModel added = MovieService.GetLatestMovie();
                return ResultService.SuccessResult<MovieModel>(added);
            }
            return ResultService.ErrorResult(CodeEnum.SqlError, "Failed to add movie");

        }

        [HttpPatch]
        [Route("Movies/{id}")]
        public JsonResult UpdateMovie([System.Web.Http.FromBody] int id, [System.Web.Http.FromBody] UpdatedMovieModel model)
        {
            MovieModel previous = MovieService.GetMovieById(id);

            model.id = id;
            if (string.IsNullOrEmpty(model.title)) model.title = previous.title;
            if (string.IsNullOrEmpty(model.description)) model.description = previous.description;
            if (string.IsNullOrEmpty(model.image)) model.image = previous.image;
            if (string.IsNullOrEmpty(model.rating.ToString())) model.rating = previous.rating;

            int result = MovieService.UpdateMovie(model);
            if (result == 0)
            {
                MovieModel updated = MovieService.GetLatestUpdatedMovie();
                return ResultService.SuccessResult<MovieModel>(updated);
            }
            return ResultService.ErrorResult(CodeEnum.SqlError, "Failed to update movie");

        }

        [HttpDelete]
        [Route("Movies/{id}")]
        public JsonResult DeleteMovie([System.Web.Http.FromBody] int id)
        {

            int result = MovieService.DeleteMovie(id);
            if (result == 0)
            {
                return ResultService.SuccessResult();
            }
            return ResultService.ErrorResult(CodeEnum.SqlError, "Failed to delete movie");

        }

    }
}