﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class CommentEntity
    {
        public int  Id { get; set; }

        public int CoctailId { get; set; }

        public int UserId { get; set; }

        public string Comment { get; set; }
    }
}
