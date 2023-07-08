using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace TrainWebApp.Pages
{
    public class WeightScaleModel : PageModel
    {
        [BindProperty]
        public string? Owner { get; set; }

        [BindProperty]
        public string? CarId { get; set; }

        [BindProperty]
        public RailCarType Type { get; set; }

        [BindProperty]
        [Display(Name = "Weight (in 100's)")]
        public int Weight { get; set; }
        public bool IsSuccess { get; set; } = false;

        public bool IsValidOwner { get; set; } = true;
        public bool IsValidCarId { get; set; } = true;
        public bool IsValidType { get; set; } = true;
        public bool IsValidWeight { get; set; } = true;

        public void OnPost(string action)
        {
            if (action == "Record")
            {
                ValidateInput();
                if (ModelState.IsValid)
                {
                    CarWeight carWeight = new CarWeight
                    {
                        Owner = Owner,
                        CarId = CarId,
                        CarType = Type,
                        ScaleWeight = Weight
                    };

                    string filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "Data",
                        "RailCarWeights.txt"
                    );

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine(carWeight.toString());
                        }

                        // Clear the form fields
                        Owner = string.Empty;
                        CarId = string.Empty;
                        Type = RailCarType.BOX_CAR;
                        Weight = 0;
                        IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, $"Error occurred while saving the record: {ex.Message}");
                    }
                }
            }
            else if (action == "Clear")
            {
                // Clear the form fields
                Owner = string.Empty;
                CarId = string.Empty;
                Type = RailCarType.BOX_CAR;
                Weight = 0;
            }
        }

        private void ValidateInput()
        {
            IsValidOwner = !string.IsNullOrEmpty(Owner);
            IsValidCarId = !string.IsNullOrEmpty(CarId);
            IsValidType = Type != RailCarType.BOX_CAR; // Adjust this condition based on your validation logic
            IsValidWeight = Weight > 0; // Adjust this condition based on your validation logic
        }
    }
}
