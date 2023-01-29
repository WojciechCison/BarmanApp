using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public IList<UserCoctailEntity> FavoriteCoctailsList { get;  set; } = new List<UserCoctailEntity>();
    }
}
