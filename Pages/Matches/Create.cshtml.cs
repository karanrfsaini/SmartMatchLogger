using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartMatchLogger.Data;
using SmartMatchLogger.Models;
using Microsoft.EntityFrameworkCore;


namespace SmartMatchLogger.Pages.Matches
{
    public class CreateModel : PageModel
    {
        private readonly SmartMatchLogger.Data.MatchContext _context;

        public CreateModel(SmartMatchLogger.Data.MatchContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Match Match { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnGetAsync()
        {
            // Load distinct opponent names from existing matches
            var opponentNames = await _context.Match
                .Select(m => m.Opponent)
                .Distinct()
                .OrderBy(name => name)
                .ToListAsync();

            ViewData["OpponentNames"] = opponentNames;

            return Page();
        }

    }
}
