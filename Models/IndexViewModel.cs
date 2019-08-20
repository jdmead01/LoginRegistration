using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistration.Models{
    public class IndexViewModel
    {
        public Register newRegister {get;set;}
        public Login newLogin {get;set;}
    }
}
