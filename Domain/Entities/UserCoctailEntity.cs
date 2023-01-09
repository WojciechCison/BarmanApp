using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserCoctailEntity
    {
        public int CoctailId { get; set; }
        public CoctailEntity Coctail { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
