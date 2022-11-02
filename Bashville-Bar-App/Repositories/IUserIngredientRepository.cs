using BashvilleBarApp.Models;
using System.Collections.Generic;

namespace BashvilleBarApp.Repositories
{
    public interface IUserIngredientRepository
    {
        void AddUserIngredient(UserIngredient userIngredient);
        void DeleteUserIngredient(int id);
        List<UserIngredient> GetAllUserIngredients(int id);
        UserIngredient GetUserIngredientById(int id);
        void UpdateUserIngredients(int userId, List<Ingredient> ingredients);
    }
}