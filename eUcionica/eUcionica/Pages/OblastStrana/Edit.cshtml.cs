using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.OblastStrana
{
    public class EditModel : PageModel
    {
        private readonly DatabaseContext.DBContextClass _context;

        public EditModel(DatabaseContext.DBContextClass context)
        {
            _context = context;
        }

        [BindProperty]
        public Oblast Oblast { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Oblast == null)
            {
                return NotFound();
            }

            var oblast =  await _context.Oblast.FirstOrDefaultAsync(m => m.ID == id);
            if (oblast == null)
            {
                return NotFound();
            }
            Oblast = oblast;
           ViewData["PredmetID"] = new SelectList(_context.Predmet, "ID", "Ime");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           /* if (!ModelState.IsValid)
            {
                return Page();
            }
           */
            _context.Attach(Oblast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OblastExists(Oblast.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Oblasti");
        }

        private bool OblastExists(int id)
        {
          return (_context.Oblast?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
