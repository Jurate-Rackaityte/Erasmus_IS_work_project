using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IS_1.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "The Username is required and should be unique.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
        //TO-DO: warn people that the password is unsafe -.-
        //  so that they would not use something they use on
        //  other places
        public User(string name, string pass)
        {
            Username = name;
            Password = pass;
        }
    }
}
