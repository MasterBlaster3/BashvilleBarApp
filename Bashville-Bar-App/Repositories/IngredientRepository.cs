using BashvilleBarApp.Models;
using BashvilleBarApp.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BashvilleBarApp.Repositories
{
    public class IngredientRepository : BaseRepository, IIngredientRepository
    {
        public IngredientRepository(IConfiguration configuration) : base(configuration) { }

        public List<Ingredient> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Name, i.Id as Id
                              
                       FROM Ingredient i

                       ORDER BY i.Name";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var ingredients = new List<Ingredient>();

                        while (reader.Read())
                        {
                            Ingredient ingredient = new Ingredient()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),

                            };
                            ingredients.Add(ingredient);

                        };
                        reader.Close();
                        return ingredients;

                    }


                }
            }
        }

        //TODO: SQL query needs work.
        public Ingredient GetIngredientById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT i.Name, i.Id,                             
                     
                       WHERE i.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Ingredient ingredient = null;
                        while (reader.Read())
                        {
                            if (ingredient == null)
                            {
                                ingredient = new Ingredient()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name"),


                                };

                            }
                        }
                        return ingredient;

                    }
                }
            }
        }


        public void UpdateIngredient(Ingredient ingredient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Ingredient
                            SET 
                                [Name] = @name,                                
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@name", ingredient.Name);
                    DbUtils.AddParameter(cmd, "@id", ingredient.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddIngredient(Ingredient ingredient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Ingredient ([Name])
                    OUTPUT INSERTED.Id
                    VALUES (@name)";
                    DbUtils.AddParameter(cmd, "@name", ingredient.Name);
                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    ingredient.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteIngredient(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Ingredient
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //TODO Create search logic
    }
}
