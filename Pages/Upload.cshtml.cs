using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using SmartMatchLogger.Models;

namespace SmartMatchLogger.Pages
{
    public class UploadModel : PageModel
    {
        private readonly SmartMatchLogger.Data.MatchContext _context;

        public UploadModel(SmartMatchLogger.Data.MatchContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IFormFile CsvFile { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CsvFile == null || CsvFile.Length == 0)
            {
                Message = "Please select a valid CSV file.";
                return Page();
            }

            try
            {
                using var reader = new StreamReader(CsvFile.OpenReadStream());
                var headerLine = await reader.ReadLineAsync();

                var expectedHeaders = new[]
                {
                    "Date","Opponent","Winner","PlayStyle","Location","Surface","Score",
                    "MatchNotes","FatigueLevel","ForehandRating","BackhandRating","ServeRating","VolleyRating"
                };

                var actualHeaders = headerLine?.Split(',');

                if (actualHeaders == null || expectedHeaders.Length != actualHeaders.Length ||
                    !expectedHeaders.SequenceEqual(actualHeaders.Select(h => h.Trim()), StringComparer.OrdinalIgnoreCase))
                {
                    Message = "CSV header is not in the expected format.";
                    return Page();
                }

                var matches = new List<Match>();

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    if (values.Length != expectedHeaders.Length)
                        continue; // Skip malformed line

                    try
                    {
                        var match = new Match
                        {
                            Date = DateTime.Parse(values[0]),
                            Opponent = values[1],
                            Winner = bool.Parse(values[2]),
                            PlayStyle = values[3],
                            Location = values[4],
                            Surface = values[5],
                            Score = values[6],
                            MatchNotes = values[7],
                            FatigueLevel = int.Parse(values[8]),
                            ForehandRating = int.Parse(values[9]),
                            BackhandRating = int.Parse(values[10]),
                            ServeRating = int.Parse(values[11]),
                            VolleyRating = int.Parse(values[12])
                        };

                        matches.Add(match);
                    }
                    catch
                    {
                        // Log or skip bad row
                        continue;
                    }
                }

                if (matches.Count > 0)
                {
                    _context.Match.AddRange(matches);
                    await _context.SaveChangesAsync();
                    Message = $"Successfully imported {matches.Count} matches!";
                }
                else
                {
                    Message = "No valid matches found to import.";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error: {ex.Message}";
            }

            return Page();
        }

    }
}
