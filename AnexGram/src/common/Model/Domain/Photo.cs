using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
