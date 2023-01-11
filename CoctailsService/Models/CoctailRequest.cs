using Domain.Entities;

namespace CoctailsService.Models
{
    public class CoctailRequest
    {
        public string Name { get; set; }

        public Dictionary<int, double> ingridients { get; set; }
    }
}
