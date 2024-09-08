using DatabaseContext;
using DatabaseEntityLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eUcionica.Pages.TestStrana
{
    public class TestModel : PageModel
    {
        private readonly DBContextClass _context;

        public TestModel(DBContextClass context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int SelectedPredmetID { get; set; }

        [BindProperty]
        public Pitanje Pitanje { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int SelectedOblastID { get; set; }

        public SelectList PredmetOptions { get; set; }
        public SelectList OblastOptions { get; set; }
        public SelectList OblastOptionsWithAll { get; set; }

        public List<Pitanje> SelectedQuestions { get; set; } = new List<Pitanje>(); 

        public void OnGet()
        {

            PredmetOptions = new SelectList(_context.Predmet, "ID", "Ime");


            var oblastsWithAll = _context.Oblast
                .Where(o => o.PredmetID == SelectedPredmetID)
                .Select(o => new SelectListItem { Value = o.ID.ToString(), Text = o.Ime })
                .ToList();
            oblastsWithAll.Insert(0, new SelectListItem { Value = "0", Text = "All" });
            OblastOptionsWithAll = new SelectList(oblastsWithAll, "Value", "Text");

        }

        public JsonResult OnGetOblasts(int predmetID)
        {
            var oblasts = _context.Oblast
                .Where(o => o.PredmetID == predmetID)
                .Select(o => new { id = o.ID, ime = o.Ime })
                .ToList();

            return new JsonResult(oblasts);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<Pitanje> pitanje;
            if (SelectedOblastID == 0)
            {

                pitanje = await _context.Pitanje
                    .Where(p => p.PredmetID == SelectedPredmetID)
                    .ToListAsync();
            }
            else
            {

                pitanje = await _context.Pitanje
                    .Where(p => p.PredmetID == SelectedPredmetID && p.OblastID == SelectedOblastID)
                    .ToListAsync();
            }

            var random = new Random();
            SelectedQuestions = pitanje.OrderBy(q => random.Next()).Take(5).ToList();
            TempData["SelectedQuestions"] = JsonConvert.SerializeObject(SelectedQuestions);
            return RedirectToPage("./GenerisaniTest");
        }

    }
}
