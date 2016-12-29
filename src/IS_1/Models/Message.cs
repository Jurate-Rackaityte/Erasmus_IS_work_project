using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace IS_1.Models
{
    public class Message
    {
        //[Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Text { get; set; }
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
        }

    }
}
