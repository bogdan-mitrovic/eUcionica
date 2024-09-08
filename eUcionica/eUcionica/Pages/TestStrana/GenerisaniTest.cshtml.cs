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
    public class GenerisaniTestModel : PageModel
    {
        private readonly DBContextClass _context;

        public GenerisaniTestModel(DBContextClass context)
        {
            _context = context;
        }
        [BindProperty]
        //public Pitanje Pitanje { get; set; } = default!;
        public List<Pitanje> SelectedQuestions { get; set; }

        public async Task OnGetAsync(/*List<Pitanje> selectedQuestions*/)
        {
            // SelectedQuestions = selectedQuestions;
            var selectedQuestionsJson = TempData["SelectedQuestions"] as string;
            SelectedQuestions = JsonConvert.DeserializeObject<List<Pitanje>>(selectedQuestionsJson);

        }
    }
}
