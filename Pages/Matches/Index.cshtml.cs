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
    public class IndexModel : PageModel
    {
        private readonly SmartMatchLogger.Data.MatchContext _context;

        public IndexModel(SmartMatchLogger.Data.MatchContext context)
        {
            _context = context;
        }

        public IList<Match> Match { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Match = await _context.Match.ToListAsync();
        }
    }
}
