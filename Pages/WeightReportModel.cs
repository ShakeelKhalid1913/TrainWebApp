using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace TrainWebApp.Pages
{
    public class WeightReportModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public List<CarWeight> RailCarWeights { get; set; } = new List<CarWeight>();

        public WeightReportModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Data", "RailCarWeights.txt");
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        if (Enum.TryParse(parts[1].Trim(), out RailCarType railCarType))
                        {
                            var railCarWeight = new CarWeight
                            {
                                CarId = parts[0].Trim(),
                                CarType = railCarType,
                                ScaleWeight = int.Parse(parts[2].Trim())
                            };
                            RailCarWeights.Add(railCarWeight);
                        }
                    }
                }
            }
        }
    }
}
