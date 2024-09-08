using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DatabaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.OblastStrana
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext.DBContextClass _context;

        public CreateModel(DatabaseContext.DBContextClass context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PredmetID"] = new SelectList(_context.Predmet, "ID", "Ime");
            return Page();
        }

        [BindProperty]
        public Oblast Oblast { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (/*!ModelState.IsValid ||*/ _context.Oblast == null || Oblast == null)
            {
                return Page();
            }

            _context.Oblast.Add(Oblast);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Oblasti");
        }
    }
}
