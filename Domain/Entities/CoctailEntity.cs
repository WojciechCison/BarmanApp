using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CoctailEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<CoctailIngridientEntity> CoctailIngridients { get; private set; } = new List<CoctailIngridientEntity>();

        public IList<UserCoctailEntity> UserCoctails { get; private set; } = new List<UserCoctailEntity>();

    }
}
