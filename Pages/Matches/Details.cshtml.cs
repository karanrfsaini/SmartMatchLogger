using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartMatchLogger.Data;
using SmartMatchLogger.Models;

namespace SmartMatchLogger.Pages.Matches
{
    public class DetailsModel : PageModel
    {
        private readonly SmartMatchLogger.Data.MatchContext _context;

        public DetailsModel(SmartMatchLogger.Data.MatchContext context)
        {
            _context = context;
        }

        public Match Match { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FirstOrDefaultAsync(m => m.Id == id);

            if (match is not null)
            {
                Match = match;

                return Page();
            }

            return NotFound();
        }
    }
}
