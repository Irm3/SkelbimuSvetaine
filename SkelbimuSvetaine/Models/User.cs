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

        [Required(ErrorMessage ="Turite įvesti savo vartotojo vardą!")]
        [StringLength(20, ErrorMessage = "Vardas negali viršyti 20 simbolių!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Turite įvesti savo slaptažodį!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Turite įvesti savo telefono numerį!")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Turite įvesti savo elektroninį paštą!")]
        [EmailAddress]
        public string Email { get; set; }

        public byte[] Icon { get; set; }

        [Required(ErrorMessage = "Turite įvesti savo miestą!")]
        public string Miestas { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
