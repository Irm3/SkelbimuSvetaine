using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SkelbimuSvetaine.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Products = new HashSet<Product>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Turite įvesti savo vartotojo vardą")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Turite įvesti savo slaptažodį")]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Icon { get; set; }
        public string Miestas { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
