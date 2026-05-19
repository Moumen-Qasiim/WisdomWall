using System;
using System.Linq;
using WisdomWall.Models;

namespace WisdomWall.Data;

public static class DbInitializer
{
    public static void Initialize(QuotesDbContext context)
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if there are already any quotes
        if (context.Quotes.Any())
        {
            return; // DB has been seeded
        }

        var quotes = new Quote[]
        {
            new Quote 
            { 
                Text = "The only limit to our realization of tomorrow is our doubts of today.", 
                Author = "Franklin D. Roosevelt", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-50) 
            },
            new Quote 
            { 
                Text = "Do what you can, with what you have, where you are.", 
                Author = "Theodore Roosevelt", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-45) 
            },
            new Quote 
            { 
                Text = "Success is not final, failure is not fatal: it is the courage to continue that counts.", 
                Author = "Winston Churchill", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-40) 
            },
            new Quote 
            { 
                Text = "The purpose of our lives is to be happy.", 
                Author = "Dalai Lama", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-35) 
            },
            new Quote 
            { 
                Text = "Life is what happens when you're busy making other plans.", 
                Author = "John Lennon", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-30) 
            },
            new Quote 
            { 
                Text = "Get busy living or get busy dying.", 
                Author = "Stephen King", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-25) 
            },
            new Quote 
            { 
                Text = "You only live once, but if you do it right, once is enough.", 
                Author = "Mae West", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-20) 
            },
            new Quote 
            { 
                Text = "Many of life's failures are people who did not realize how close they were to success when they gave up.", 
                Author = "Thomas A. Edison", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-15) 
            },
            new Quote 
            { 
                Text = "If you want to live a happy life, tie it to a goal, not to people or things.", 
                Author = "Albert Einstein", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-10) 
            },
            new Quote 
            { 
                Text = "Never let the fear of striking out keep you from playing the game.", 
                Author = "Babe Ruth", 
                CreatedAt = DateTime.UtcNow.AddMinutes(-5) 
            }
        };

        context.Quotes.AddRange(quotes);
        context.SaveChanges();
    }
}
