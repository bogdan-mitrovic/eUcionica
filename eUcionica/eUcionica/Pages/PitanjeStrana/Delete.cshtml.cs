using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DatabaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.PitanjeStrana
{
    public class DeleteModel : PageModel
    {
        private readonly DatabaseContext.DBContextClass _context;

        public DeleteModel(DatabaseContext.DBContextClass context)
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

            var pitanje = await _context.Pitanje.FirstOrDefaultAsync(m => m.ID == id);

            if (pitanje == null)
            {
                return NotFound();
            }
            else 
            {
                Pitanje = pitanje;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pitanje == null)
            {
                return NotFound();
            }
            var pitanje = await _context.Pitanje.FindAsync(id);

            if (pitanje != null)
            {
                Pitanje = pitanje;
                _context.Pitanje.Remove(Pitanje);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Pitanja");
        }
    }
}
