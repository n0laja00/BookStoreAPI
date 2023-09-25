﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    /// <summary>
    /// Model for Books
    /// </summary>
    public class Book
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Title { get; set; }
        [Required]
        [MinLength(1)]
        public string Author { get; set; }
        [Required]
        public int Year { get; set; }
        public string? Publisher { get; set; }
        public string? Description { get; set; }
    }
}