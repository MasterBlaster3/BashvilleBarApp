﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BashvilleBarApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
