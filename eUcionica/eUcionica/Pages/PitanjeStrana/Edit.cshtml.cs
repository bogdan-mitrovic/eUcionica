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

namespace eUcionica.Pages.PitanjeStrana
{
    public class EditModel : PageModel
    {
        private readonly DatabaseContext.DBContextClass _context;

        public EditModel(DatabaseContext.DBContextClass context)
        {
            _context = context;
        }

        [BindProperty]
        public Pitanje Pitanje { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pitanje == null)
            {
                return NotFound();
            }

            var pitanje =  await _context.Pitanje.FirstOrDefaultAsync(m => m.ID == id);
            if (pitanje == null)
            {
                return NotFound();
            }
            Pitanje = pitanje;
           ViewData["OblastID"] = new SelectList(_context.Oblast, "ID", "Ime");
           ViewData["PredmetID"] = new SelectList(_context.Predmet, "ID", "Ime");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
          /*  if (!ModelState.IsValid)
            {
                return Page();
            }
          */
            _context.Attach(Pitanje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PitanjeExists(Pitanje.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Pitanja");
        }

        private bool PitanjeExists(int id)
        {
          return (_context.Pitanje?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
