using System;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Pages
{
    public class ManageBooksModel : PageModel
    {
        private readonly BookDbContext _db;

        public ManageBooksModel(BookDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            Book = await _db.Books.FindAsync(id);

            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (ModelState.IsValid)
            {
                ValidateInputs();

                if (ModelState.ErrorCount > 0)
                {
                    return Page();
                }
                
                var bookFromDb = await _db.Books.FindAsync(Book.Id);
                bookFromDb.Name = Book.Name;
                bookFromDb.Author = Book.Author;
                bookFromDb.ReleaseDate = Book.ReleaseDate;
                bookFromDb.PageCount = Book.PageCount;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (ModelState.IsValid)
            {
                ValidateInputs();

                if (ModelState.ErrorCount > 0)
                {
                    return Page();
                }

                _db.Books.Add(Book);

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

        public void ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Book.Name) || Book.Name.Length < 3 || Book.Name.Length > 30)
            {
                ModelState.AddModelError("Book.Name", "Book name must be in range from 3 to 30 letters!");
            }
            if (string.IsNullOrWhiteSpace(Book.Author) || Book.Author.Length < 3 || Book.Author.Length > 30)
            {
                ModelState.AddModelError("Book.Author", "Author name must be in range from 3 to 30 letters!");
            }
            if (Book.ReleaseDate > DateTime.Now)
            {
                ModelState.AddModelError("Book.ReleaseDate", "Release date must be valid!");
            }
            if (Book.PageCount <= 0)
            {
                ModelState.AddModelError("Book.PageCount", "Number of pages must be greater than zero!");
            }
        }
    }
}
