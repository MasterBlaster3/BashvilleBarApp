using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BashvilleBarApp.Models;
using BashvilleBarApp.Repositories;
using System;

namespace BashvilleBarApp.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class IngredientController : ControllerBase
        {
            private readonly IIngredientRepository _ingredientRepository;

            public IngredientController(IIngredientRepository ingredientRepository)
            {
                _ingredientRepository = ingredientRepository;
            }

            [HttpGet]
            public IActionResult Get()
            {
                return Ok(_ingredientRepository.GetAll());
            }

            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                return Ok(_ingredientRepository.GetIngredientById(id));
            }

            [HttpPost]
            public IActionResult Post(Ingredient ingredient)
            {
                try
                {
                    _ingredientRepository.AddIngredient(ingredient);
                    return CreatedAtAction("Get", new { id = ingredient.Id }, ingredient);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            [HttpPut("{id}")]
            public IActionResult Put(int id, Ingredient ingredient)
            {
                if (id != ingredient.Id)
                {
                    return BadRequest();
                }
                _ingredientRepository.UpdateIngredient(ingredient);
                return NoContent();
            }

            [HttpDelete("delete/{id}")]
            public IActionResult Delete(int id)
            {
                _ingredientRepository.DeleteIngredient(id);
                return NoContent();
            }

            //TODO: This is to be able to search for ingredients. 
            //public IActionResult Search(string q)
            //{
            //    return Ok(_ingredientRepository.Search(q));
            //}

        }




    
}
