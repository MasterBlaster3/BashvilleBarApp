using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BashvilleBarApp.Models;
using BashvilleBarApp.Repositories;
using System;
using System.Collections.Generic;

namespace BashvilleBarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserIngredientController : ControllerBase
    {
        private readonly IUserIngredientRepository _userIngredientRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserIngredientController(IUserIngredientRepository userIngredientRepository, IUserProfileRepository userProfileRepository)
        {
            _userIngredientRepository = userIngredientRepository;
            _userProfileRepository = userProfileRepository;
        }

        //Get endpoint to get user saved ingredient objects
        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = GetCurrentUserProfile();
            return Ok(_userIngredientRepository.GetAllUserIngredients(currentUser.Id));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_userIngredientRepository.GetUserIngredientById(id));
        }

        //Post endpoint to add an ingredient to the user bar
        [HttpPost]
        public IActionResult Post(UserIngredient userIngredient)
        {
            try
            {
                var currentUser = GetCurrentUserProfile();
                userIngredient.UserProfileId = currentUser.Id;
                _userIngredientRepository.AddUserIngredient(userIngredient);
                return CreatedAtAction("Get", new { id = userIngredient.Id }, userIngredient);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //Put endpoint to update user saved ingredient object
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, UserIngredient userIngredient)
        //{
        //    if (id != userIngredient.Id)
        //    {
        //        return BadRequest();
        //    }
        //    _userIngredientRepository.UpdateUserIngredient(userIngredient);
        //    return NoContent();
        //}

        //Delete endpoint to delete user saved ingredient object
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _userIngredientRepository.DeleteUserIngredient(id);
            return NoContent();
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }

        [HttpPost("UpdateUsersIngredients")]
        public IActionResult Post(List<Ingredient> ingredients)
        {
            var currentUser = GetCurrentUserProfile();
            _userIngredientRepository.UpdateUserIngredients(currentUser.Id, ingredients);
            return Ok(ingredients);
        }
    }
}
