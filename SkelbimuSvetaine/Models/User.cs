using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required(ErrorMessage ="Turite sukurti savo vartotojo vardą!")]
        [StringLength(20, ErrorMessage = "Vardas negali viršyti 20 simbolių!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Turite sukurti savo slaptažodį!")]
        [StringLength(50, ErrorMessage = "Turi būti mažiausiai 6 simboliai!", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Patvirtinkite slaptažodį!")]
        [Compare("Password", ErrorMessage = "Slaptažodis turi sutapti!")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Turite įvesti savo telefono numerį!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Turite įvesti savo elektroninį paštą!")]
        [EmailAddress(ErrorMessage = "Turite įvesti teisingą el. paštą!")]
        public string Email { get; set; }

        public byte[] Icon { get; set; }

        [Required(ErrorMessage = "Turite įvesti savo miestą!")]
        public string Miestas { get; set; }

        public string Role { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
