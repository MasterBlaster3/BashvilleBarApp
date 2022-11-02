using BashvilleBarApp.Models;
using System.Collections.Generic;

namespace BashvilleBarApp.Repositories
{
    public interface IIngredientRepository
    {
        void AddIngredient(Ingredient ingredient);
        void DeleteIngredient(int id);
        List<Ingredient> GetAll();
        Ingredient GetIngredientById(int id);
        void UpdateIngredient(Ingredient ingredient);
    }
}