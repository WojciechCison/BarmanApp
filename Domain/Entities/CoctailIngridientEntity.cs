using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CoctailIngridientEntity
    {


        public int CoctailId { get; set; }
        public CoctailEntity Coctail { get; set; }

        public int IngridientId { get; set; }
        public IngridientEntity Ingridient { get; set; }

        public double Dose { get; set; }
    }
}
