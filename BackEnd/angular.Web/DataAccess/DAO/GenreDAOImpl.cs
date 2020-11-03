using angular.Web.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angular.Web.DataAccess.DAO
{
    public class GenreDAOImpl : GenreDAO
    {
        String connectionString;
        const String RETRIEVE_BOOKS = "";
        const String RETRIEVE_GENRES = 
            "SELECT * " +
            "FROM [dbo].[Genre]"; 

        public GenreDAOImpl(IConfiguration iconfig){
            connectionString = iconfig.GetValue<String>("ConnectionStrings:DefaultConnection");
        }   
        public List<Book> retrieveBooks(int genreId)
        {
            return null; 
        }
        public List<Genre> retrieveGenres()
        {
            List<Genre> results = new List<Genre>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(RETRIEVE_GENRES, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Genre genre = new Genre();
                        genre.Id = (int)reader["Id"];
                        genre.Name = reader["Name"].ToString();
                        results.Add(genre); 
                    }
                }
                finally { reader.Close(); }

            }
            return results; 
        }
    }
}
