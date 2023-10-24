﻿using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Web_API.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}
    }
}