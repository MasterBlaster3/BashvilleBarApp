using BashvilleBarApp.Models;
using System.Collections.Generic;

namespace BashvilleBarApp.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        List<UserProfile> GetAllUsers();
        UserProfile GetByUserId(int userId);
        void Add(UserProfile userProfile);
    }
}