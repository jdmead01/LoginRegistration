using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoginRegistration.Models{
    public class Login
    {
        [Key]
        public int loginId{get; set;}
        [Required]
        [EmailAddress]
        public string email {get; set;}
        [Required]
        public string password {get; set;}
        public DateTime createdAt {get; set;} = DateTime.Now;
        public DateTime updatedAt{get; set;} = DateTime.Now;
    }
}















