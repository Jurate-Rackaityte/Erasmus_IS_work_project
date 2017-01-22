using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class DbInitializer
    {
        public static void Initialize(Models.ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            // Look for any users
            if (context.Users.Any())
            {
                return;     // DB has been seeded
            }

            var users = new User[]
            {
                new User("HelloWorld", "NoAdminRuleRules"),
                new User("test", "test123"),
                new User("meow", "woofwoof")
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var messages = new Message("Hello World! Welcome to Chatton.", "HelloWorld");
            context.Messages.Add(messages);
            context.SaveChanges();
        }
    }
}
