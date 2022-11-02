using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BashvilleBarApp.Models;
using BashvilleBarApp.Utils;
using System;
using System.Collections.Generic;

namespace BashvilleBarApp.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"
                        SELECT up.Id, Up.FirebaseUserId, up.Name, up.Email, up.CreateDateTime,  up.IsAdmin
                          FROM UserProfile up
                         WHERE FirebaseUserId = @FirebaseuserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);
                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),                            
                            IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
                        };
                    }
                    reader.Close();
                    return userProfile;
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserProfile (FirebaseUserId, Name, CreateDateTime, Email, IsAdmin)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @Name, @Email, @IsAdmin, @CreateDateTime)";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", userProfile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@IsAdmin", userProfile.IsAdmin);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", userProfile.CreateDateTime);
                    

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<UserProfile> GetAllUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT up.Id AS UserId, up.Name, up.IsAdmin, up.Email, up.FirebaseUserId, up.CreateDateTime, 
                         FROM UserProfile as up
                         ORDER BY up.Name";
                    var reader = cmd.ExecuteReader();
                    var users = new List<UserProfile>();

                    while (reader.Read())
                    {
                        users.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "UserId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            
                            IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
                        });
                    }

                    reader.Close();
                    return users;
                }
            }
        }

        public UserProfile GetByUserId(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id AS UserId, up.Name, up.IsAdmin, up.Email, up.CreateDateTime
                        FROM UserProfile as up
                        WHERE up.Id = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        UserProfile profile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "UserId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "email"),
                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                            
                            IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
                        };
                        reader.Close();
                        return profile;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
