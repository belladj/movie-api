using Microsoft.VisualStudio.TestTools.UnitTesting;
using movies_api;
using movies_api.Models;
using movies_api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace movies_api.Tests.Controllers
{
    [TestClass]
    class MovieControllerTest
    {
        [TestMethod]
        public void GetMovieListTest()
        {
            MovieController controller = new MovieController();
            var test = controller.GetMovieList();
            string json = (string) test.Data;
            BaseResult<List<MovieModel>> result = JsonConvert.DeserializeObject<BaseResult<List<MovieModel>>>(json);
            Assert.AreEqual(0,result.Code);
        }

        [TestMethod]
        public void GetMovieTest(int id)
        {
            MovieController controller = new MovieController();
            var test = controller.GetMovie(id);
            string json = (string)test.Data;
            BaseResult<MovieModel> result = JsonConvert.DeserializeObject<BaseResult<MovieModel>>(json);
            Assert.AreEqual(0, result.Code);
        }

        [TestMethod]
        public void AddMovieTest()
        {
            MovieController controller = new MovieController();
            NewMovieModel model = new NewMovieModel
            {
                title = "Test Movie",
                description = "for testing purposes",
                rating = 5,
                image = ""
            };
            var test = controller.AddMovie(model);
            string json = (string)test.Data;
            BaseResult<MovieModel> result = JsonConvert.DeserializeObject<BaseResult<MovieModel>>(json);
            Assert.AreEqual(0, result.Code);
        }

        [TestMethod]
        public void UpdateMovieTest()
        {
            MovieController controller = new MovieController();
            UpdatedMovieModel model = new UpdatedMovieModel
            {
                id = 1,
                title = "Test Edit Movie",
                description = "for testing purposes",
                rating = 5,
                image = ""
            };
            var test = controller.UpdateMovie(1,model);
            string json = (string)test.Data;
            BaseResult<MovieModel> result = JsonConvert.DeserializeObject<BaseResult<MovieModel>>(json);
            Assert.AreEqual(0, result.Code);
        }

        [TestMethod]
        public void DeleteMovieTest(int id)
        {
            MovieController controller = new MovieController();
            var test = controller.DeleteMovie(id);
            string json = (string)test.Data;
            BaseResult<object> result = JsonConvert.DeserializeObject<BaseResult<object>>(json);
            Assert.AreEqual(0, result.Code);
        }
    }
}
