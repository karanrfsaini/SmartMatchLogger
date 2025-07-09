using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using SmartMatchLogger.Data;
using SmartMatchLogger.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartMatchLogger.Pages
{
    public class StatsModel : PageModel
    {
        private readonly SmartMatchLogger.Data.MatchContext _context;

        public StatsModel(SmartMatchLogger.Data.MatchContext context)
        {
            _context = context;
        }


        

        [BindProperty(SupportsGet = true)]
        public List<string> SelectedStrokes { get; set; } = new();

        public List<int> DataPoints { get; set; } = new();
        public List<string> Labels { get; set; } = new();
        public Dictionary<string, List<int>> AllStrokeData { get; set; } = new();



        public void OnGet()
        {
            var matches = _context.Match.OrderBy(m => m.Date).ToList();
            int total = matches.Count;
            int step = total > 12 ? total / 12 : 1;

            for (int i = 0; i < total; i += step)
            {
                var match = matches[i];
                Labels.Add(match.Date.ToShortDateString());

                foreach (var stroke in SelectedStrokes)
                {
                    int rating = stroke switch
                    {
                        "Forehand" => match.ForehandRating,
                        "Backhand" => match.BackhandRating,
                        "Serve" => match.ServeRating,
                        "Volley" => match.VolleyRating,
                        _ => 0
                    };

                    if (!AllStrokeData.ContainsKey(stroke))
                        AllStrokeData[stroke] = new List<int>();

                    AllStrokeData[stroke].Add(rating);
                }
            }
        }

    }
}
