using Domain.Entities;

namespace CoctailsService.Models
{
    public class CoctailResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<IngridientEntity> Ingridients { get; set; }
    }
}
