using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DatabaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.OblastStrana
{
    public class DeleteModel : PageModel
    {
        private readonly DatabaseContext.DBContextClass _context;

        public DeleteModel(DatabaseContext.DBContextClass context)
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

            var oblast = await _context.Oblast.FirstOrDefaultAsync(m => m.ID == id);

            if (oblast == null)
            {
                return NotFound();
            }
            else 
            {
                Oblast = oblast;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Oblast == null)
            {
                return NotFound();
            }
            var oblast = await _context.Oblast.FindAsync(id);

            if (oblast != null)
            {
                Oblast = oblast;
                _context.Oblast.Remove(Oblast);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Oblasti");
        }
    }
}
