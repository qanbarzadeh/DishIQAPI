// See https://aka.ms/new-console-template for more information
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up the dependency injection container
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Your_Connection_String_Here")); // Replace with your actual connection string

            var serviceProvider = services.BuildServiceProvider();

            // Get an instance of AppDbContext
            using var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

            // Perform an action that triggers DbContext initialization
            var users = dbContext.Users.ToListAsync().Result;
            Console.WriteLine($"Number of users: {users.Count}");
        }
    }
}