using movies_api.Models;
using movies_api.Libraries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace movies_api.Services
{
    public class MovieService
    {
        private static string Add_Movie = "Insert Into Movies(Title,Description,Rating,Image) Values(@Title,@Description,@Rating,@Image)";
        private static string Update_Movie = "Update Movies Set Title=@Title,Description=@Description,Rating=@Rating,Image=@Image,updatedAt=getdate() Where ID=@ID";
        private static string Delete_Movie = "Delete From Movies Where ID=@ID";
        private static string Select_Movies = "Select * From Movies";
        private static string Select_Movie_ID = "Select * From Movies Where ID=@ID";
        private static string Select_Latest_Movie = "Select * From Movies Where ID=(Select max(ID) From Movies)";
        private static string Select_Latest_Updated_Movie = "Select * From Movies Where updatedAt=(Select max(updatedAt) From Movies)";

        public static List<MovieModel> GetMovies()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(SqlHelper.GetInstance().ConnectString);
                conn.Open();
                using (SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(conn, Select_Movies))
                {
                    List<MovieModel> results = SelectMovieByCmdTxt(sqlDataReader);
                    if (results != null && results.Count > 0)
                        return results;
                }
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return null;
        }

        public static MovieModel GetMovieById(int id)
        {
            SqlConnection conn = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@ID", id)
                };
                conn = new SqlConnection(SqlHelper.GetInstance().ConnectString);
                conn.Open();
                using (SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(conn, Select_Movie_ID, paras))
                {
                    List<MovieModel> results = SelectMovieByCmdTxt(sqlDataReader);
                    if (results != null && results.Count > 0)
                        return results[0];
                }
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return null;
        }

        public static MovieModel GetLatestMovie()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(SqlHelper.GetInstance().ConnectString);
                conn.Open();
                using (SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(conn, Select_Latest_Movie))
                {
                    List<MovieModel> results = SelectMovieByCmdTxt(sqlDataReader);
                    if (results != null && results.Count > 0)
                        return results[0];
                }
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return null;
        }

        public static MovieModel GetLatestUpdatedMovie()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(SqlHelper.GetInstance().ConnectString);
                conn.Open();
                using (SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(conn, Select_Latest_Updated_Movie))
                {
                    List<MovieModel> results = SelectMovieByCmdTxt(sqlDataReader);
                    if (results != null && results.Count > 0)
                        return results[0];
                }
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return null;
        }

        public static int AddMovie(NewMovieModel model)
        {
            SqlConnection conn = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@Title",model.title),
                    new SqlParameter("@Description",model.description),
                    new SqlParameter("@Rating",model.rating),
                    new SqlParameter("@Image",model.image),
                };
                conn = new SqlConnection(SqlHelper.GetInstance().ConnectString);
                conn.Open();
                int results = SqlHelper.Execute(conn, Add_Movie, paras);
                if (results == 1)
                    return 0;
            }
            catch (Exception ex) { }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return -1;
        }

        public static int UpdateMovie(UpdatedMovieModel model)
        {
            SqlConnection conn = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {

                    new SqlParameter("@ID",model.id),
                    new SqlParameter("@Title",model.title),
                    new SqlParameter("@Description",model.description),
                    new SqlParameter("@Rating",model.rating),
                    new SqlParameter("@Image",model.image),
                };
                conn = new SqlConnection(SqlHelper.GetInstance().ConnectString);
                conn.Open();
                int result = SqlHelper.Execute(conn, Update_Movie, paras);
                if (result == 1)
                    return 0;
            }
            catch (Exception ex) { }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return -1;
        }

        public static int DeleteMovie(int id)
        {
            SqlConnection conn = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@ID",id),
                };
                conn = new SqlConnection(SqlHelper.GetInstance().ConnectString);
                conn.Open();
                int result = SqlHelper.Execute(conn, Delete_Movie, paras);
                if (result == 1)
                    return 0;
            }
            catch (Exception ex) { }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            return -1;
        }

        private static SqlParameter[] GetParameters(MovieModel model)
        {
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@ID",model.id),
                new SqlParameter("@Title",model.title),
                new SqlParameter("@Description",model.description),
                new SqlParameter("@Rating",model.rating),
                new SqlParameter("@Image",model.image),
                new SqlParameter("@createdAt",model.created_at),
                new SqlParameter("@updatedAt",model.updated_at),
            };
            return paras;
        }

        private static List<MovieModel> SelectMovieByCmdTxt(SqlDataReader sdr)
        {
            List<MovieModel> results = new List<MovieModel>();

            while (sdr.Read())
            {
                MovieModel model = new MovieModel();
                model.id = Tools.ConvertObjectToIn32(sdr["ID"]);
                model.title = Tools.ConvertObjectToString(sdr["Title"]);
                model.description = Tools.ConvertObjectToString(sdr["Description"]);
                model.rating = Tools.ConvertObjectToFloat(sdr["Rating"]);
                model.image = Tools.ConvertObjectToString(sdr["Image"]);
                model.created_at = Tools.ConvertObjectToDateTime(sdr["createdAt"]);
                model.updated_at = Tools.ConvertObjectToDateTime(sdr["updatedAt"]);
                results.Add(model);
            }
            sdr.Close();
            sdr.Dispose();
            return results;
        }
    }
}