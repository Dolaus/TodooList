using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TodooList.Data;

namespace TodooList.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDBContext>>()))
            {
                // Look for any movies.
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }

                context.User.AddRange(
                    new User
                    {
                       Name = "Petro",
                       Year = 2003,
                       TodoList = new List<ToDo> { new ToDo {Description="Go to the shop"} }
                    },
                    new User
                    {
                        Name = "Oleh",
                        Year = 2002,
                        TodoList = new List<ToDo> { new ToDo { IsComplete =false,Description="Change hair" },
                           new ToDo { IsComplete = false, Description = "Persil" } }

                    },
                    new User
                    {
                        Name = "Andriy",
                        Year = 2002
                    },
                    new User
                    {
                        Name = "Arsen",
                        Year = 2002
                    }
                );
                context.SaveChanges();
            }
        }
    }
}