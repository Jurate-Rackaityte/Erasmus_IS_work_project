using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Message
    {
        //can there be multiple keys?
        //[Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Text { get; set; }
        // hope this will increment automatically -.-
        [Key]
        public DateTime Date { get; set; }

        public Message(string text, string username)
        {
            if (!string.IsNullOrEmpty(text) 
                && !string.IsNullOrEmpty(username))
            {
                Date = DateTime.Now;        //gives a LOCAL time
                Text = text;
                Username = username;
            }
            else
            {
                //error message
            }
        }

        public Message() { }

    }
}
