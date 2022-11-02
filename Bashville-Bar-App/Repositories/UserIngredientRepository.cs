using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using BashvilleBarApp.Models;
using BashvilleBarApp.Utils;
using Microsoft.Data.SqlClient;

namespace BashvilleBarApp.Repositories
{
    public class UserIngredientRepository : BaseRepository, IUserIngredientRepository
    {
        public UserIngredientRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserIngredient> GetAllUserIngredients(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ui.Id AS Id, UserProfileId, IngredientId, Name
                        FROM UserIngredient ui
                        JOIN Ingredient i ON ui.IngredientId = i.id
                        WHERE UserProfileId = @id
                        ORDER BY Name";
                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();

                    var userIngredients = new List<UserIngredient>();

                        while (reader.Read())
                        {
                            UserIngredient userIngredient = new UserIngredient()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Ingredient = new Ingredient()
                                {
                                    Id = DbUtils.GetInt(reader, "IngredientId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                }
                            };
                            userIngredients.Add(userIngredient);

                        };
                        reader.Close();
                        return userIngredients;

                    


                }
            }
        }

        //TODO: SQL query needs work.
        public UserIngredient GetUserIngredientById(int id)
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
                        UserIngredient userIngredient = null;
                        while (reader.Read())
                        {
                            if (userIngredient == null)
                            {
                                userIngredient = new UserIngredient()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                    Ingredient = new Ingredient()
                                    {
                                        Id = DbUtils.GetInt(reader, "IngredientId"),
                                        Name = DbUtils.GetString(reader, "Name"),
                                    }
                                };

                            }
                        }
                        return userIngredient;

                    }
                }
            }
        }


        public void UpdateUserIngredients(int userId, List<Ingredient> ingredients)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM UserIngredient WHERE UserProfileId = @UserProfileId";
                    DbUtils.AddParameter(cmd, "@UserProfileId", userId);
                    cmd.ExecuteNonQuery();
                }
                foreach (var ingredient in ingredients)
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            INSERT INTO UserIngredient (UserProfileId, IngredientId)
                            VALUES (@UserProfileId, @ingredientId)";

                        DbUtils.AddParameter(cmd, "@UserProfileId", userId);
                        DbUtils.AddParameter(cmd, "@ingredientId", ingredient.Id);
                        

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AddUserIngredient(UserIngredient userIngredient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO UserIngredient (IngredientId, UserProfileId)
                    OUTPUT INSERTED.Id
                    VALUES (@IngredientId, @UserProfileId)";
                    DbUtils.AddParameter(cmd, "@IngredientId", userIngredient.IngredientId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", userIngredient.UserProfileId);
                    userIngredient.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        //TODO: refactor method below to delete all user ingredients prior to the add ingredients. 
        public void DeleteUserIngredient(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM UserIngredient
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
