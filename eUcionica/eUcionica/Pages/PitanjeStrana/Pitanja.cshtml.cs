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
    public class PitanjaModel : PageModel
    {
        private readonly DatabaseContext.DBContextClass _context;

        public PitanjaModel(DatabaseContext.DBContextClass context)
        {
            _context = context;
        }

        public IList<Pitanje> Pitanje { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Pitanje != null)
            {
                Pitanje = await _context.Pitanje
                .Include(p => p.Oblast)
                .Include(p => p.Predmet).ToListAsync();
            }
        }
    }
}
