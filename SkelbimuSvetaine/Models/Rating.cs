using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SkelbimuSvetaine.Models
{
    public partial class Rating : IValidatableObject
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Value < 0 || Value > 5)
            {
                yield return new ValidationResult(
                    "Balai tik nuo 1 iki 5",
                    new[] { nameof(Value) });
            }
        }
    }
}
