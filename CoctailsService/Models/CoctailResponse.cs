namespace CoctailsService.Models
{
    public class CoctailResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Dictionary<int, double> Ingridients { get; set; }
    }
}
