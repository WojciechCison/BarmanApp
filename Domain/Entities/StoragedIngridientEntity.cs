using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StoragedIngridientEntity
    {
        public int Id { get; set; }

        public int IngridientId { get; set; }

        [JsonIgnore]
        public IngridientEntity Ingridient { get; set; }

        public double Quantity { get; set; }
    }
}
