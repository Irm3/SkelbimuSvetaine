using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SkelbimuSvetaine.Models
{
    public partial class Product : IValidatableObject
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Turite sukurti savo pavadinimą!")]
        [StringLength(100, ErrorMessage = "Pavadinimas negali viršyti 100 simbolių!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Turite sukurti savo aprašymą!")]
        [StringLength(200, ErrorMessage = "Pavadinimas negali viršyti 200 simbolių!")]
        public string Description { get; set; }

    
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedTimestamp { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price < 0)
            {
                yield return new ValidationResult(
                    "Kaina turi būti didesnė už 0",
                    new[] { nameof(Price) });
            }
        }
    }
}
