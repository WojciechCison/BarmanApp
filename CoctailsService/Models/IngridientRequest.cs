using Microsoft.AspNetCore.Mvc;

namespace CoctailsService.Models
{
    public class IngridientRequest 
    {
        public string Name { get; set; }

        public string Unit { get; set; }
    }
}
