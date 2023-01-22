using Microsoft.AspNetCore.Mvc;

namespace CoctailsService.Models
{
    public class IngridientRequest 
    {
        public string Token { get; set; }
        public string Name { get; set; }

        public string Unit { get; set; }
    }
}
