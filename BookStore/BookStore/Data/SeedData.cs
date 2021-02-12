using System;
using System.Linq;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public static class SeedData
    {
        public static void Initialize(BookDbContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Books', RESEED, 0);"); // reset autoincrement 
            
            #region SeedInitialData

            context.Books.AddRange(
                new Book
                {
                    Author = "Stephen King",
                    Name = "The Stand",
                    ReleaseDate = DateTime.Parse("1986-9-15"),
                    PageCount = 1138
                },
                new Book
                {
                    Author = "Stephen King",
                    Name = "Carrie",
                    ReleaseDate = DateTime.Parse("1974-4-5"),
                    PageCount = 199
                },
                new Book
                {
                    Author = "Ernest Hemingway",
                    Name = "The Old Man and the Sea",
                    ReleaseDate = DateTime.Parse("1952-9-1"),
                    PageCount = 127
                },
                new Book
                {
                    Author = "Charles Dickens",
                    Name = "David Copperfield",
                    ReleaseDate = DateTime.Parse("1850-11-1"),
                    PageCount = 669
                },
                new Book
                {
                    Author = "Kurt Vonnegut",
                    Name = "Slaughterhouse‑Five",
                    ReleaseDate = DateTime.Parse("1969-3-31"),
                    PageCount = 1089
                }
            );

            #endregion

            context.SaveChanges();
        }
    }
}

