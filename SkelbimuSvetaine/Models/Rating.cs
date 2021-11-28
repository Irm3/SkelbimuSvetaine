using System;
using System.Collections.Generic;

#nullable disable

namespace SkelbimuSvetaine.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
