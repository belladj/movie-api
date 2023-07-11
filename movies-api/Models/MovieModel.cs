using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movies_api.Models
{
    public class MovieModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float rating { get; set; }
        public string image { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class NewMovieModel
    {
        public string title { get; set; }
        public string description { get; set; } = "";
        public float rating { get; set; }
        public string image { get; set; } = "";
    }

    public class UpdatedMovieModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float rating { get; set; }
        public string image { get; set; }
    }

}