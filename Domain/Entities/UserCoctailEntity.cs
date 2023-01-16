using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserCoctailEntity
    {
        public int CoctailId { get; set; }

        [JsonIgnore]
        public CoctailEntity Coctail { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public UserEntity User { get; set; }
    }
}
