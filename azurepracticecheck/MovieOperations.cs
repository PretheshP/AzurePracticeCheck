using azurepracticecheck.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace azurepracticecheck
{
    public class MovieOperations
    {
        public static string sqlDataSource = "Server=(localdb)\\MSSQLLocalDB;Database=Movie;Trusted_Connection=True;MultipleActiveResultSets=true;";

        public static IEnumerable<MovieList> GetConnection()
        {

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                List<MovieList> Items = new List<MovieList>();
                var list = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy", "Adventure", "Thriller" };


                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select * from MovieList";
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Items.Add(new MovieList
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Title = rd["Title"].ToString(),
                            BoxOffice = rd["BoxOffice"].ToString(),
                            Active = Convert.ToBoolean(rd["Active"]),
                            DateOfLaunch = Convert.ToDateTime(rd["DateOfLaunch"]),
                            HasTeaser = Convert.ToBoolean(rd["HasTeaser"]),
                            GenreId = Convert.ToInt32(rd["GenreId"]),
                            GenreType = rd["GenreType"].ToString(),
                        });
                    }
                    con.Close();
                }


                return Items;
            }

        }
        public static void InsertMov(MovieList movie)//admin insert movie 
        {
            var list = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy", "Adventure", "Thriller" };
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                SqlConnection conn = new SqlConnection(sqlDataSource);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO MovieList(Title,BoxOffice,Active,DateOfLaunch,HasTeaser,GenreId,GenreType) Values (@Title,@BoxOffice,@Active,@DateOfLaunch,@HasTeaser,@GenreId,@GenreType)";
                sqlCmd.Connection = conn;
                sqlCmd.Parameters.AddWithValue("@Title", movie.Title);
                sqlCmd.Parameters.AddWithValue("@BoxOffice", movie.BoxOffice);
                sqlCmd.Parameters.AddWithValue("@Active", movie.Active);
                sqlCmd.Parameters.AddWithValue("@DateOfLaunch", movie.DateOfLaunch);
                sqlCmd.Parameters.AddWithValue("@HasTeaser", movie.HasTeaser);
                sqlCmd.Parameters.AddWithValue("@GenreId", movie.GenreId);
                sqlCmd.Parameters.AddWithValue("@GenreType", movie.GenreType);

                conn.Open();
                sqlCmd.ExecuteNonQuery();
                conn.Close();

            }
        }
        //update,put movie by admin
        public static void Update(int id, MovieList movie)
        {
            var list = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy", "Adventure", "Thriller" };
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                SqlCommand cmd = new SqlCommand("Update MovieList set Title=@Title,BoxOffice=@BoxOffice,Active=@Active,DateOfLaunch=@DateOfLaunch,HasTeaser=@HasTeaser,GenreId=@GenreId,GenreType=@GenreType where id=@Id", con);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Title", movie.Title);
                cmd.Parameters.AddWithValue("@BoxOffice", movie.BoxOffice);
                cmd.Parameters.AddWithValue("@Active", movie.Active);
                cmd.Parameters.AddWithValue("@DateOfLaunch", movie.DateOfLaunch);
                cmd.Parameters.AddWithValue("@HasTeaser", movie.HasTeaser);
                cmd.Parameters.AddWithValue("@GenreId", movie.GenreId);
                cmd.Parameters.AddWithValue("@GenreType", movie.GenreType);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        public static void Insert(UserDetails user)//add newuser 
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO UserDetails(UserName,FirstName,LastName,Password,ConfirmPassword) Values (@UserName,@FirstName,@LastName,@Password,@ConfirmPassword)";
            sqlCmd.Connection = con;


            
            sqlCmd.Parameters.AddWithValue("@UserName", user.UserName);
            sqlCmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            sqlCmd.Parameters.AddWithValue("@LastName", user.LastName);
            sqlCmd.Parameters.AddWithValue("@Password", user.Password);
            sqlCmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);

            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();

        }
        public static List<UserDetails> UserList()
        {
            List<UserDetails> users = new List<UserDetails>();

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select * from UserDetails";
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        users.Add(new UserDetails
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            UserName = rd["UserName"].ToString(),
                            FirstName = rd["FirstName"].ToString(),
                            LastName = rd["LastName"].ToString(),
                            Password = rd["Password"].ToString(),
                            ConfirmPassword = rd["ConfirmPassword"].ToString()

                        });
                    }
                    con.Close();
                }

            }
            return users;


        }
        public static void InsertIntoFavorites(List<Favorites> favorite)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Favorites(MovieListId,UserId) Values (@MovieListId,@UserId)";
            sqlCmd.Connection = con;
            con.Open();
            foreach (var i in favorite)
            {
                //sqlCmd.Parameters.AddWithValue("@Id", i.Id);
                sqlCmd.Parameters.AddWithValue("@MovieListId", i.MovieListId);
                sqlCmd.Parameters.AddWithValue("@UserId", i.UserId);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Parameters.Clear();
            }

            con.Close();
        }
        public static List<MovieList> FavoriteList(int userid, ref int rowCount)
        {

            List<MovieList> Items = new List<MovieList>();
            List<int> list = new List<int>();

            var l = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy", "Adventure", "Thriller" };


            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select MovieListId from Favorites where Userid = @userid";
                    cmd.Parameters.AddWithValue("@userid", userid);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        list.Add(Convert.ToInt32(rd["MovieListId"]));
                    }
                    rd.Close();
                    foreach (var i in list)
                    {

                        cmd.CommandText = "select * from MovieList where Id = @i";
                        cmd.Parameters.AddWithValue("@i", i);
                        SqlDataReader rd1 = cmd.ExecuteReader();
                        while (rd1.Read())
                        {
                            Items.Add(new MovieList
                            {
                                Id = Convert.ToInt32(rd1["Id"]),
                                Title = rd1["Title"].ToString(),
                                BoxOffice = rd1["BoxOffice"].ToString(),
                                Active = Convert.ToBoolean(rd1["Active"]),
                                DateOfLaunch = Convert.ToDateTime(rd1["DateOfLaunch"]),
                                HasTeaser = Convert.ToBoolean(rd1["HasTeaser"]),
                                GenreId = Convert.ToInt32(rd1["GenreId"]),
                                GenreType = rd1["GenreType"].ToString()
                            });
                            rowCount += Convert.ToInt32(rd1["Id"]);

                        }
                        cmd.Parameters.Clear();
                        rd1.Close();
                    }
                    con.Close();

                }

            }
            return Items;

        }
        public static string Delete(int favId)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from Favorites where Id =@favId";
            sqlCmd.Connection = con;
            con.Open();
            sqlCmd.Parameters.AddWithValue("@favId", favId);
            int i = sqlCmd.ExecuteNonQuery();
            if (i >= 1)
                return "record deleted";
            else
                return "no record";

        }
        //delete movie
        public static string Deletemov(int MovieId)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from MovieList where Id =@MovieId";
            sqlCmd.Connection = con;
            con.Open();
            sqlCmd.Parameters.AddWithValue("@MovieId", MovieId);
            int i = sqlCmd.ExecuteNonQuery();
            if (i >= 1)
                return "record deleted";
            else
                return "no record";

        }
    }
}
