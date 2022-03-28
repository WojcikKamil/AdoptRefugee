﻿using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 4)]
        public string? Password { get; set; }
        [Required]
        public string? Status { get; set; }
    }
}
