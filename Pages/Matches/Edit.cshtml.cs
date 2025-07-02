using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartMatchLogger.Data;
using SmartMatchLogger.Models;

namespace SmartMatchLogger.Pages.Matches
{
    public class EditModel : PageModel
    {
        private readonly SmartMatchLogger.Data.MatchContext _context;

        public EditModel(SmartMatchLogger.Data.MatchContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Match Match { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match =  await _context.Match.FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }
            Match = match;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(Match.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.Id == id);
        }
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (Match == null || Match.Id == 0)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(Match.Id);

            if (match != null)
            {
                _context.Match.Remove(match);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

    }
}
