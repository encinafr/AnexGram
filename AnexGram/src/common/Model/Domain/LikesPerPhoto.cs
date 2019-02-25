﻿using Model.Domain.DbHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain
{
    public class LikesPerPhoto : AuditEntity,ISoftDeleted
    {
        public long Id { get; set; }

        public Photo Photo { get; set; }
        public int PhotoId { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public bool Deleted { get; set; }
    }
}
